using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    private JoystickMovement joystickMovement;
    private Animator anim;
    private Rigidbody rb;

    private void Start()
    {
        joystickMovement = JoystickMovement.Instance;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (joystickMovement != null) joystickMovement.Movement(moveSpeed, turnSpeed, transform, anim);
    }
}
