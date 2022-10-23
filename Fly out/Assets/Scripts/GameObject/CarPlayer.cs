using UnityEngine;
using System.Collections;

public class CarPlayer : MonoBehaviour
{
    [SerializeField] private WheelCollider frontLeftWhellCollider;
    [SerializeField] private WheelCollider frontRightWhellCollider;
    [SerializeField] private WheelCollider rearLeftWhellCollider;
    [SerializeField] private WheelCollider rearRightWhellCollider;
    [SerializeField] private float maxSteer = 30;
    [SerializeField] private float maxAccel = 2500;
    [SerializeField] private float maxBrake = 50;
    [SerializeField] private Transform centerOfMass;
    private bool _isBanControl;
    private InputController _inputController;


    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
        _inputController = FindObjectOfType<InputController>();
    }

    private void FixedUpdate()
    {
        if (_isBanControl) return;
        Move(_inputController.Horizontal);
        Turn(_inputController.Vertical);
    }

    public void BanControl() => _isBanControl = true;

    private void Move(float steer)
    {
        frontLeftWhellCollider.steerAngle = 
            frontRightWhellCollider.steerAngle = steer * maxSteer;
    }

    private void Turn(float accel)
    {
        if (accel != 0)
        {
            frontLeftWhellCollider.brakeTorque =
                frontRightWhellCollider.brakeTorque =
                    rearLeftWhellCollider.brakeTorque =
                        rearRightWhellCollider.brakeTorque = 0;

            frontLeftWhellCollider.motorTorque =
                 frontRightWhellCollider.motorTorque = accel * maxAccel;
        }
        else
        {
            frontLeftWhellCollider.brakeTorque =
                frontRightWhellCollider.brakeTorque =
                    rearLeftWhellCollider.brakeTorque =
                        rearRightWhellCollider.brakeTorque = maxBrake;
        }
    }
}
