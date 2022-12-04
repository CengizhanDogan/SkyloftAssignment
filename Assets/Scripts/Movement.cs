using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float turnSpeed;

    protected JoystickInput joystick;

    private void Start()
    {
        joystick = JoystickInput.Instance;
    }
}
