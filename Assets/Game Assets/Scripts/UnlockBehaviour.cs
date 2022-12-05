using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UnlockBehaviour : MonoBehaviour, IInteractable, IExitable
{
    private bool exited;

    [SerializeField] private TextMeshProUGUI costTextMesh;
    [SerializeField] private Image fillImage;
    [SerializeField] private List<GameObject> unlockObjects = new List<GameObject>();

    private List<IUnlockable> unlockables = new List<IUnlockable>();

    private int unlockCost;
    private int startCost;
    private float fillCount;

    private void Awake()
    {
        GetUnlockables();

        costTextMesh.text = unlockCost.ToString();
    }

    private void GetUnlockables()
    {
        foreach (var unlockObject in unlockObjects)
        {
            if (unlockObject.TryGetComponent(out IUnlockable unlockable))
            {
                unlockables.Add(unlockable);
                unlockCost += unlockable.GetCost();
            }
            else
            {
                Debug.LogError($"Object {unlockObject} has not a script that has IUnlockable interface on it!", this);
            }
        }

        startCost = unlockCost;
    }

    public void Interact(Interactor interactor)
    {
        var stackManager = interactor.GetComponent<StackManager>();

        if (!stackManager && unlockables.Count == 0) return;
        exited = false;
        StartCoroutine(SpendMetalToUnlock(stackManager));
    }

    private IEnumerator SpendMetalToUnlock(StackManager stackManager)
    {
        yield return new WaitForSeconds(.5f);
        while (!exited)
        {
            if (stackManager.StackCount > 0 && unlockCost > 0)
            {
                stackManager.SpendMetal(this);
                unlockCost -= 1;
                fillCount++;
                costTextMesh.text = unlockCost.ToString();
                DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, fillCount / startCost, 0.25f);
            }
            if (unlockCost <= 0 && unlockCost != -11)
            {
                foreach (var unlockable in unlockables)
                {
                    unlockable.GetUnlocked();
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
