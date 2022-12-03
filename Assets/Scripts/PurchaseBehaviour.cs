using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PurchaseBehaviour : MonoBehaviour, IInteractable, IExitable
{
    private bool exited;

    [SerializeField] private TextMeshProUGUI costTextMesh;
    [SerializeField] private Image fillImage;
    [SerializeField] private List<GameObject> purchaseObjects = new List<GameObject>();

    private List<IPurchasable> purchasables = new List<IPurchasable>();

    private int purchaseCost;
    private int startCost;
    private float fillCount;

    private void Awake()
    {
        GetPurchasable();

        purchaseCost = purchasables[0].GetCost();
        startCost = purchaseCost;

        costTextMesh.text = purchaseCost.ToString();
    }

    private void GetPurchasable()
    {
        foreach (var purchaseObject in purchaseObjects)
        {
            if (purchaseObject.TryGetComponent(out IPurchasable purchasable))
            {
                purchasables.Add(purchasable);
            }
            else
            {
                Debug.LogError($"Object {purchaseObject} has not a script that has IPurchasable interface on it!", this);
            }
        }
    }

    public void Interact(Interactor interactor)
    {
        var stackManager = interactor.GetComponent<StackManager>();

        if (!stackManager && purchasables.Count == 0) return;
        exited = false;
        StartCoroutine(SpendMetalToPurchase(stackManager));
    }

    private IEnumerator SpendMetalToPurchase(StackManager stackManager)
    {
        yield return new WaitForSeconds(.5f);
        while (!exited)
        {
            if (stackManager.StackCount > 0 && purchaseCost > 0)
            {
                stackManager.SpendMetal(this);
                purchaseCost -= 1;
                fillCount++;
                costTextMesh.text = purchaseCost.ToString();
                DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, fillCount / startCost, 0.25f);
            }
            if (purchaseCost <= 0)
            {
                foreach (var purchasable in purchasables)
                {
                    purchasable.GetPurchased();
                }
                gameObject.SetActive(false);
                DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, 1, 0.25f);
                yield break;
            }
            yield return new WaitForSeconds(2f / startCost);
        }
    }
    public void Exit()
    {
        exited = true;
    }
}
