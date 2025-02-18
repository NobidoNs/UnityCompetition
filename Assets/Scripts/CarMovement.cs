using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheel;
    [SerializeField] private WheelCollider frontRightWheel;

    [Header("Settings")]
    [SerializeField] private float maxMotorTorque = 1000f;
    [SerializeField] private float maxSteeringAngle = 30f;

    private float motorInput;
    private float steeringInput;

    public void SetInputs(float steering, float motor)
    {
        steeringInput = steering;
        motorInput = motor;
    }

    private void FixedUpdate()
    {
        UpdateSteering();
        UpdateMotor();
    }

    private void UpdateSteering()
    {
        float steeringAngle = maxSteeringAngle * steeringInput;
        frontLeftWheel.steerAngle = steeringAngle;
        frontRightWheel.steerAngle = steeringAngle;
    }

    private void UpdateMotor()
    {
        float motorTorque = maxMotorTorque * motorInput;
        frontLeftWheel.motorTorque = motorTorque;
        frontRightWheel.motorTorque = motorTorque;
    }
}