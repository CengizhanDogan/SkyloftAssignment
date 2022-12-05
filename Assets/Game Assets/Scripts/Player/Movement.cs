using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Parent script of a script that uses player input by joystick to move.

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float turnSpeed;

    protected JoystickInput joystick;

    private void Start()
    {
        joystick = JoystickInput.Instance;
    }

    protected void Clamp()
    {
        Vector3 clampPos = transform.position;
        clampPos.x = Mathf.Clamp(transform.position.x, -100f, 100f);
        clampPos.z = Mathf.Clamp(transform.position.z, -20, 180);

        transform.position = clampPos;
    }
}
