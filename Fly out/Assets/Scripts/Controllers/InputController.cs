using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool TouchDownForSelectForce { get { return _touchController.TouchDown; } }

    public bool TouchUpForSelectForce { get { return _touchController.TouchUp; } }

    public float Horizontal { get { return _variableJoystick.Horizontal; } }

    public float Vertical { get { return _variableJoystick.Vertical; } }

    private TouchController _touchController;
    private VariableJoystick _variableJoystick;

    private void Start()
    {
        _touchController = FindObjectOfType<TouchController>();
        _variableJoystick = FindObjectOfType<VariableJoystick>();
    }
}
