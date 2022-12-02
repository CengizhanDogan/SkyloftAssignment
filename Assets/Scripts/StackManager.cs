using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackManager : MonoBehaviour
{
    private List<Metal> metals = new List<Metal>();

    [SerializeField] private Transform stackTransform;
    [SerializeField] private List<Transform> splineTransforms = new List<Transform>();
    [SerializeField] private float stackDistance;
    [SerializeField] private int stackCount;

    public void CollectMetal(Metal metal)
    {
        if (metals.Count >= stackCount) return;
        metal.transform.DOLocalRotate(stackTransform.eulerAngles, 0.25f);
        metals.Add(metal);
        StartCoroutine(metal.CollectMovement(stackTransform, splineTransforms, stackDistance, metals.Count, transform));
    }

    
}
