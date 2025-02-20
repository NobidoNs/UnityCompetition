using UnityEngine;

public class WheelSteering : MonoBehaviour
{
    public GameObject LeftWheel, RightWheel, SteeringWheel;

    private float totalSteeringAngle = 0f;
    private float lastSteeringRotation = 0f;
    public float chet = 0;

    void Update()
    {
        float currentRotation = SteeringWheel.transform.rotation.z;
        if (currentRotation > 180) currentRotation -= 360;
        float deltaRotation = currentRotation - lastSteeringRotation;
        if (deltaRotation > 180) deltaRotation -= 360;
        if (deltaRotation < -180) deltaRotation += 360;
        totalSteeringAngle += deltaRotation;
        chet = totalSteeringAngle;
        if(totalSteeringAngle > 360 || totalSteeringAngle < -360){
             SteeringWheel.transform.rotation = Quaternion.Euler(0, -90, Mathf.Clamp(currentRotation, -180, 180));
        }
        totalSteeringAngle = Mathf.Clamp(totalSteeringAngle, -540f, 540f);
        float wheelRotation = (totalSteeringAngle / 540f) * 45f;
        LeftWheel.transform.localRotation = Quaternion.Euler(-90, wheelRotation, 0);
        RightWheel.transform.localRotation = Quaternion.Euler(-90, wheelRotation, 0);
        lastSteeringRotation = currentRotation;
    }
}