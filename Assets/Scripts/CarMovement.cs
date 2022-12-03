using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : Movement
{
    private Car car;
    private void Awake()
    {
        car = GetComponent<Car>();
    }
    private void Update()
    {
        if (joystickMovement != null && car.IsPurchased) joystickMovement.Movement(moveSpeed, turnSpeed, transform, true);
    }
}
