using UnityEngine;

public class Car_Control2 : MonoBehaviour
{
    private MechanicalSystem link1;
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

    private float throttleInput;
    private float steeringInput;
    private bool isBraking;

    [Header("Состояние")]
    public bool isPlayerInCar = false;

    public int current = 1;
    public float[] gearRatios = { 0, -2.5f, 0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f };
    private void Update()
    {

        if (isPlayerInCar)
        {
            HandleInput();
            UpdateWheelTransforms();
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
        throttleInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }
    void MoveVehicle()
    {
        float motorTorque = throttleInput * maxMotorTorque;
        motorTorque = -motorTorque;
        rearLeftWheel.motorTorque = motorTorque * gearRatios[current];
        rearRightWheel.motorTorque = motorTorque * gearRatios[current];
    }
    void ApplyBrakes()
    {
        float appliedBrake = isBraking ? brakeForce : 0f;
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
