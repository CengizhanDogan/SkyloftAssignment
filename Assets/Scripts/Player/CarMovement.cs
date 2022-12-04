using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : Movement
{
    private Car car;
    private bool haveDriver;

    [SerializeField] private List<Transform> wheels = new List<Transform>();
    [SerializeField] private List<Transform> frontWheels = new List<Transform>();

    private void OnEnable()
    {
        EventManager.OnDriveEvent.AddListener(SetDriver);
        EventManager.OnExitCarEvent.AddListener(SetDriver);
    }

    private void SetDriver()
    {
        haveDriver = false;
    }
    private void SetDriver(Transform t, float f)
    {
        haveDriver = true;
    }
    private void Awake()
    {
        car = GetComponent<Car>();
    }
    private void Update()
    {
        MoveCar();
    }

    private void MoveCar()
    {
        if (joystick != null && car.IsPurchased && haveDriver)
        {
            float x = joystick.HorizontalInput;
            float z = joystick.VerticalInput;

            if (x == 0 && z == 0)
            {
                foreach (var frontWheel in frontWheels)
                {
                    frontWheel.localRotation = Quaternion.Slerp(frontWheel.localRotation,
                        Quaternion.identity, turnSpeed * 2 * Time.deltaTime);
                }

                return;
            }

            float speed = new Vector2(x, z).magnitude;

            transform.position += transform.forward * moveSpeed * Time.deltaTime * speed;

            Vector3 direction = (Vector3.forward * z + Vector3.right * x);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime * speed);

            RotateWheels(direction , speed);
        }
    }

    private void RotateWheels(Vector3 direction, float speed) 
    {
        foreach (var frontWheel in frontWheels)
        {
            frontWheel.rotation = Quaternion.Slerp(frontWheel.rotation, Quaternion.LookRotation(direction), turnSpeed * 2 * Time.deltaTime * speed);
        }
        foreach (var wheel in wheels)
        {
            wheel.Rotate(Vector3.right * turnSpeed * 200 * Time.deltaTime * speed);
        }
    }
}
