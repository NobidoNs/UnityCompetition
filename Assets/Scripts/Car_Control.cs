using UnityEngine;

public class CarControl : MonoBehaviour
{
    private MechanicalSystem link1;
    public WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
    public Transform frontLeftMesh, frontRightMesh, rearLeftMesh, rearRightMesh;
    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float brakeForce = 3000f;
    public Transform steeringWheel;
    public int current;
    public float[] gearRatios = { -2.5f, 0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f };
    private float throttleInput, steeringInput;
    private bool isBraking;
    public bool isPlayerInCar = false;

    private void Start()
    {
        link1 = GetComponent<MechanicalSystem>();
        UpdateWheelTransforms(); 
    }

    private void Update()
    {
        current = link1.current;
        if (isPlayerInCar)
        {
            HandleInput();
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerInCar)
        {
            MoveVehicle();
            ApplyBrakes();
            UpdateWheelTransforms();
        }
    }

    void HandleInput()
    {
        throttleInput = Input.GetAxis("Vertical");
        steeringInput = GetSteeringInputFromWheel();
        isBraking = Input.GetKey(KeyCode.Space);
    }

    float GetSteeringInputFromWheel()
    {
        float wheelAngle = steeringWheel.localEulerAngles.z;
        if (wheelAngle > 180) wheelAngle -= 360;
        return Mathf.Clamp(wheelAngle / 1080f, -1f, 1f);
    }

    void MoveVehicle()
    {
        float motorTorque = throttleInput * maxMotorTorque;
        if (current > 0 && current < gearRatios.Length) 
        {
            motorTorque *= gearRatios[current];
        }
        if (current == 0) 
        {
            rearLeftWheel.motorTorque = -motorTorque;
            rearRightWheel.motorTorque = -motorTorque;
        }
        else if (current == 1) 
        {
            rearLeftWheel.motorTorque = 0;
            rearRightWheel.motorTorque = 0;
        }
        else if (current >= 2) 
        {
            rearLeftWheel.motorTorque = motorTorque;
            rearRightWheel.motorTorque = motorTorque;
        }
        float steerAngle = steeringInput * maxSteeringAngle;
        frontLeftWheel.steerAngle = steerAngle;
        frontRightWheel.steerAngle = steerAngle;
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
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        mesh.position = position;
        mesh.rotation = rotation * Quaternion.Euler(0, 0, 90);
    }
}
