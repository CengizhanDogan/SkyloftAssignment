using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    private List<Metal> metals = new List<Metal>();
    [SerializeField] private Transform stackTransform;
    [SerializeField] private float stackDistance; 
    [SerializeField] private int stackCount;

    public void CollectMetal(Metal metal)
    {
        if (metals.Count >= stackCount) return;

        var stackPos = stackTransform.position + Vector3.up * metals.Count * stackDistance;

        metal.transform.position = stackPos;
        metal.transform.rotation = stackTransform.rotation;
        metal.transform.SetParent(transform);
        metals.Add(metal);
    }
}
