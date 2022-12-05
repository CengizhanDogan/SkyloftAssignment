using UnityEngine;
using DG.Tweening;
using System;

public class DrivePanel : MonoBehaviour, IUnlockable, IInteractable
{
    private Vector3 scale;

    [SerializeField] private GameObject vehicleObject;
    private IDriveable vehicle;

    private Collider coll;
    private void Start()
    {
        scale = transform.localScale;
        coll = GetComponent<Collider>();

        transform.localScale = Vector3.zero;

        GetVehicle();
    }

    private void GetVehicle()
    {
        if (!vehicleObject)
        {
            Debug.LogError($"Object {vehicleObject} has not found!", this);
            return;
        }
        if (vehicleObject.TryGetComponent(out IDriveable vehicle))
        {
            this.vehicle = vehicle;
        }
        else
        {
            Debug.LogError($"Object {vehicleObject} has not a script that has IDriveable interface on it!", this);
        }
    }

    public int GetCost()
    {
        return 0;
    }

    public void GetUnlocked()
    {
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => coll.enabled = true); ;
    }
    private void OnEnable()
    {
        EventManager.OnExitVehicleEvent.AddListener(()=> coll.enabled = true);
    }
    public void Interact(Interactor interactor)
    {
        if (!interactor.TryGetComponent(out Character character) || !vehicleObject) return;
        coll.enabled = false;

        //When interacted this method sends the character to the vehicle and calls OnDriveEvent.

        vehicle.SetDriver(character);

        vehicle.GetStackManager(out var stackManager);
        vehicle.DriveSeat(out var driveSeat);

        character.StackManager.TransferMetal(stackManager);

        character.Rb.isKinematic = true;
        character.Coll.enabled = false;

        character.transform.DOMove(driveSeat.position, 0.5f)
            .OnComplete(()=> 
            {
                interactor.transform.SetParent(driveSeat);
                EventManager.OnDriveEvent.Invoke(vehicleObject.transform, 85f);
            });

        character.transform.DORotate(driveSeat.eulerAngles, 0.5f);
        character.transform.DOScale(0, 0.75f);
    }
}
