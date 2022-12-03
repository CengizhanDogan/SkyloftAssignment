using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour, IPurchasable
{
    [SerializeField] private int cost;

    private Vector3 scale;
    private void Start()
    {
        scale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    public int GetCost()
    {
        return cost;
    }

    public void GetPurchased()
    {
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce);
    }
}
