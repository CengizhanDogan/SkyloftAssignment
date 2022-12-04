using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour, IPurchasable
{
    [SerializeField] private int cost;

    [SerializeField] private Transform driveSeat;
    public Transform DriveSeat => driveSeat;

    private Character driver;
    private Collider coll;
    private Rigidbody rb;
    private StackManager stackManager;
    public StackManager StackManager => stackManager;

    private Vector3 scale;
    private Vector3 startPos;
    private Vector3 startRot;

    private bool isPurchased;
    public bool IsPurchased => isPurchased;

    private void Start()
    {
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        stackManager = GetComponent<StackManager>();

        scale = transform.localScale;
        startPos = transform.position;
        startRot = transform.eulerAngles;
        transform.localScale = Vector3.zero;
    }
    private void OnEnable()
    {
        EventManager.OnExitCarEvent.AddListener(ResetCar);
    }
    public int GetCost()
    {
        return cost;
    }

    public void GetPurchased()
    {
        isPurchased = true;
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => coll.enabled = true);
    }

    private void ResetCar()
    {
        StackManager.TransferMetal(driver.StackManager);

        rb.isKinematic = true;
        coll.enabled = false;
        transform.DOScale(0, 0.25f).OnComplete(() =>
        {
            transform.position = startPos;
            transform.eulerAngles = startRot;
            transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                rb.isKinematic = true;
                coll.enabled = true;
            });
        });
    }
    public void SetDriver(Character character)
    {
        driver = character;
    }
}
