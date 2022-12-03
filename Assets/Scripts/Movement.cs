using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float turnSpeed;

    protected JoystickMovement joystickMovement;

    private void Start()
    {
        joystickMovement = JoystickMovement.Instance;
    }
}
