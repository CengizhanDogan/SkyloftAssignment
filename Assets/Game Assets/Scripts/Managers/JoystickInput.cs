using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    private FloatingJoystick joystick;

    #region Singleton
    public static JoystickInput Instance { get; private set; }
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

    public float HorizontalInput => joystick.Horizontal;
    public float VerticalInput => joystick.Vertical;
}
