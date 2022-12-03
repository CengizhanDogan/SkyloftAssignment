using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour, IPurchasable
{
    [SerializeField] private int cost;

    [SerializeField]private Transform driveSeat;
    public Transform DriveSeat => driveSeat;
    private Vector3 scale;

    private bool isPurchased;
    public bool IsPurchased => isPurchased;

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
        isPurchased = true;
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce);
    }

}
