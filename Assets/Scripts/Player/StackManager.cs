using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackManager : MonoBehaviour
{
    private List<Metal> metals = new List<Metal>();

    [SerializeField] private Transform stackTransform;
    [SerializeField] private List<Transform> splineTransforms = new List<Transform>();
    [SerializeField] private float stackDistance = 1f;
    [SerializeField] private float stackSpeed = 2f;
    [SerializeField] private int stackCount;
    public int StackCount => metals.Count;
    public bool StackIsFull => metals.Count >= stackCount;

    public void CollectMetal(Metal metal)
    {
        metal.transform.DOLocalRotate(stackTransform.eulerAngles, 0.25f);
        metals.Add(metal);
        StartCoroutine(metal.MetalMovement(stackTransform, splineTransforms, stackDistance, metals.Count, transform, stackSpeed, false));
    }
    public void SpendMetal(PurchaseBehaviour pb)
    {
        if (metals.Count == 0) return;

        var metal = metals[metals.Count - 1];

        metals.Remove(metal);
        StartCoroutine(metal.MetalMovement(pb.transform, splineTransforms, stackDistance, metals.Count, transform, stackSpeed, true));
    }
    public void TransferMetal(StackManager stackManager)
    {
        if (metals.Count == 0) return;

        for (int i = metals.Count - 1; i >= 0; i--)
        {
            var metal = metals[i];
            if (stackManager.StackIsFull) return;
            metal.transform.SetParent(null);
            metals.Remove(metal);
            stackManager.CollectMetal(metal);
        }
    }
}
