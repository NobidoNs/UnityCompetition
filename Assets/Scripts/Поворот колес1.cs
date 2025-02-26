using Unity.VisualScripting;
using UnityEngine;

public class WheelSteering : MonoBehaviour
{
    public GameObject LeftWheel;
    public GameObject RightWheel;
    public GameObject SteeringWheel;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public GameObject RulePoint;
    public GameObject car;

    private float totalSteeringAngle = 0f;
    private float lastSteeringRotation = 0f;
    public float maxSteeringAngle = 45f;

    void Update()
    {
        float currentRotation = -SteeringWheel.transform.localEulerAngles.z;
        if (currentRotation > 180) currentRotation -= 360;
        float deltaRotation = currentRotation - lastSteeringRotation;
        if (deltaRotation > 180) deltaRotation -= 360;
        if (deltaRotation < -180) deltaRotation += 360;
        totalSteeringAngle += deltaRotation;
        totalSteeringAngle = Mathf.Clamp(totalSteeringAngle, -540f, 540f);
        float wheelRotation = (totalSteeringAngle / 540f) * maxSteeringAngle;       
        frontLeftWheelCollider.steerAngle = wheelRotation;
        frontRightWheelCollider.steerAngle = wheelRotation;
        lastSteeringRotation = currentRotation;
        SteeringWheel.transform.position = RulePoint.transform.position;
    }
}