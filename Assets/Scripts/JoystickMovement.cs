using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    private FloatingJoystick joystick;

    #region Singleton
    public static JoystickMovement Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    void Start()
    {
        joystick = GetComponent<FloatingJoystick>();
    }

    public void Movement(float moveSpeed, float turnSpeed, Transform transform)
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        if (horizontal == 0 && vertical == 0) return;

        Vector3 movePos = new Vector3(horizontal * moveSpeed * Time.deltaTime, 0,
            vertical * moveSpeed * Time.deltaTime);
        transform.position += movePos;

        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
    }
}
