

using UnityEngine;
using UnityEngine.InputSystem;

public class Car_Control2 : MonoBehaviour
{

    float triggerValue, gripValue; 
    private MechanicalSystem link1;
    private CarEnterExitVR link2;

    [Header("Колёса (Wheel Colliders)")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header("Модели колёс (Wheel Mesh)")]
    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    [Header("Параметры автомобиля")]
    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float brakeForce = 3000f;

    [Header("VR Controls")]
    public InputActionReference accelerateAction;
    public InputActionReference brakeAction;
    

    private bool isAccelerating = false;
    private bool isBraking = false;

    [Header("Состояние")]
    public bool isPlayerInCar = false;

    public int current = 1;
    public float[] gearRatios = { 0, -2.5f, 0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f };

    private void Update()
    {
        if (isPlayerInCar)
        {
            accelerateAction.action.Enable();
            brakeAction.action.Enable();
            HandleInput();
            UpdateWheelTransforms();
        }else{
            accelerateAction.action.Disable();
            brakeAction.action.Disable();
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerInCar)
        {
            MoveVehicle();
            ApplyBrakes();
        }
    }

   void HandleInput()
    {
        triggerValue = accelerateAction.action.ReadValue<float>();
        gripValue = brakeAction.action.ReadValue<float>();
    }

    void MoveVehicle()
    {
        float motorTorque = triggerValue * maxMotorTorque;
        motorTorque = -motorTorque;
        rearLeftWheel.motorTorque = motorTorque * gearRatios[current];
        rearRightWheel.motorTorque = motorTorque * gearRatios[current];
    }

    void ApplyBrakes()
    {
        float appliedBrake = gripValue * brakeForce ;
        frontLeftWheel.brakeTorque = appliedBrake;
        frontRightWheel.brakeTorque = appliedBrake;
        rearLeftWheel.brakeTorque = appliedBrake;
        rearRightWheel.brakeTorque = appliedBrake;
    }

    void UpdateWheelTransforms()
    {
        UpdateWheel(frontLeftWheel, frontLeftMesh);
        UpdateWheel(frontRightWheel, frontRightMesh);
        UpdateWheel(rearLeftWheel, rearLeftMesh);
        UpdateWheel(rearRightWheel, rearRightMesh);
    }

    void UpdateWheel(WheelCollider collider, Transform mesh)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        mesh.position = position;
        mesh.rotation = rotation * Quaternion.Euler(0, 0, 90);
    }
}
