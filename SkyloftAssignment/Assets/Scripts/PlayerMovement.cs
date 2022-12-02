using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    private JoystickMovement joystickMovement;

    private void Start()
    {
        joystickMovement = JoystickMovement.Instance;
    }

    private void Update()
    {
        if (joystickMovement != null) joystickMovement.Movement(moveSpeed, turnSpeed, transform);
    }
}
