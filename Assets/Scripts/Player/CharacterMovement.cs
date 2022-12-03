using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : Movement
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (joystickMovement != null) joystickMovement.Movement(moveSpeed, turnSpeed, transform, anim);
    }
}
