using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToxicFamilyGames.YandexSDK;

public class InputController : MonoBehaviour
{
    public bool TouchDownForSelectForce { get { return _isMobile ? _touchController.TouchDown : Input.GetKeyDown(KeyCode.Space); } }

    public bool TouchUpForSelectForce { get { return _isMobile ? _touchController.TouchUp : Input.GetKeyUp(KeyCode.Space); } }

    public float Horizontal { get { return _isMobile ? _variableJoystick.Horizontal : Input.GetAxis("Horizontal"); } }

    public float Vertical { get { return _isMobile ? _variableJoystick.Vertical : Input.GetAxis("Vertical"); } }

    private bool _isMobile;
    private TouchController _touchController;
    private VariableJoystick _variableJoystick;

    private void Start()
    {
        _isMobile = AuthorizationYandex.IsMobile();
        _touchController = FindObjectOfType<TouchController>();
        if (_isMobile) _variableJoystick = FindObjectOfType<VariableJoystick>();
        else FindObjectOfType<VariableJoystick>().gameObject.SetActive(false);
    }
}
