using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour, IUnlockable, IDriveable
{
    [SerializeField] private int cost;

    [SerializeField] private Transform driveSeat;

    private Character driver;
    private Collider coll;
    private Rigidbody rb;
    private StackManager stackManager;

    private Vector3 scale;
    private Vector3 startPos;
    private Vector3 startRot;

    private bool isUnlocked;
    public bool IsUnlocked => isUnlocked;

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
        EventManager.OnExitVehicleEvent.AddListener(ResetCar);
    }
    public int GetCost()
    {
        return cost;
    }

    public void GetUnlocked()
    {
        isUnlocked = true;
        rb.isKinematic = false;
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => coll.enabled = true);
    }

    private void ResetCar()
    {
        //Sends car to first position.
        stackManager.TransferMetal(driver.StackManager);

        rb.isKinematic = true;
        coll.enabled = false;
        transform.DOScale(0, 0.25f).OnComplete(() =>
        {
            transform.position = startPos;
            transform.eulerAngles = startRot;
            transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                rb.isKinematic = false;
                coll.enabled = true;
            });
        });
    }
    public void SetDriver(Character character)
    {
        driver = character;
    }

    void IDriveable.DriveSeat(out Transform driveSeat)
    {
        driveSeat = this.driveSeat;
    }

    public void GetStackManager(out StackManager stackManager)
    {
        stackManager = this.stackManager;
    }
}
