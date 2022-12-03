using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : Movement
{
    private Car car;
    private bool haveDriver;
    private void OnEnable()
    {
        EventManager.OnDriveEvent.AddListener(GetDriver);
    }

    private void OnDisable()
    {
        EventManager.OnDriveEvent.RemoveListener(GetDriver);
    }

    private void GetDriver(Transform t, float f)
    {
        haveDriver = true;
    }
    private void Awake()
    {
        car = GetComponent<Car>();
    }
    private void Update()
    {
        if (joystickMovement != null && car.IsPurchased && haveDriver) joystickMovement.Movement(moveSpeed, turnSpeed, transform, true);
    }
}
