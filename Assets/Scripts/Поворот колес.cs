using UnityEngine;

public class WheelSteering : MonoBehaviour
{
    public GameObject LeftWheel, RightWheel, SteeringWheel;

    private float totalSteeringAngle = 0f;
    private float lastSteeringRotation = 0f;

    void Update()
    {
        float currentRotation = SteeringWheel.transform.localEulerAngles.z;
        if (currentRotation > 180) currentRotation -= 360;
        float deltaRotation = currentRotation - lastSteeringRotation;
        if (deltaRotation > 180) deltaRotation -= 360;
        if (deltaRotation < -180) deltaRotation += 360;
        totalSteeringAngle += deltaRotation;
        totalSteeringAngle = Mathf.Clamp(totalSteeringAngle, -540f, 540f);
        float wheelRotation = (totalSteeringAngle / 540f) * 45f;
        LeftWheel.transform.localRotation = Quaternion.Euler(-90, wheelRotation, 0);
        RightWheel.transform.localRotation = Quaternion.Euler(-90, wheelRotation, 0);
        lastSteeringRotation = currentRotation;
    }
}
