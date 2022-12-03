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
    [SerializeField] private GameObject purchaseObject;

    private IPurchasable purchasable;

    private int purchaseCost;
    private int startCost;
    private float fillCount;

    private void Awake()
    {
        GetPurchasable();

        purchaseCost = purchasable.GetCost();
        startCost = purchaseCost;

        costTextMesh.text = purchaseCost.ToString();
    }

    private void GetPurchasable()
    {
        if (purchaseObject.TryGetComponent(out IPurchasable purchasable))
        {
            this.purchasable = purchasable;
        }
        else
        {
            Debug.LogError($"Object {purchaseObject} has not a script that has IPurchasable interface on it!", this);
        }
    }

    public void Interact(Interactor interactor)
    {
        var stackManager = interactor.GetComponent<StackManager>();

        if (!stackManager && purchasable == null) return;
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
                Debug.Log("FillCount: " + fillCount);
                Debug.Log("Value: " + fillCount / startCost);
                Debug.Log("Amount: " + fillImage.fillAmount);
                DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, fillCount / startCost, 0.25f);
            }
            if (purchaseCost <= 0)
            {
                purchasable.GetPurchased();
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
