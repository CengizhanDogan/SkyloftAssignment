using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : Movement
{
    private Animator anim;

    private bool canMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.OnDriveEvent.AddListener(StopCharacter);
        EventManager.OnExitCarEvent.AddListener(StartCharacter);
    }
    private void StopCharacter(Transform t, float f)
    {
        canMove = false;
    }
    private void StartCharacter()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!canMove) return;
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (joystick != null)
        {
            float x = joystick.HorizontalInput;
            float z = joystick.VerticalInput;

            if (anim)
            {
                var speed = new Vector3(x, 0, z).magnitude;
                anim.SetFloat("Speed", speed);
            }

            if (x == 0 && z == 0) return;


            Vector3 movePos = new Vector3(x * moveSpeed * Time.deltaTime, 0,
                z * moveSpeed * Time.deltaTime);
            transform.position += movePos;

            Vector3 direction = (Vector3.forward * z + Vector3.right * x);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        }
    }
}
