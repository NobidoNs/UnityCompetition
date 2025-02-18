using System.Collections.Generic;
using UnityEngine;

public class Car_Control : MonoBehaviour
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

    [Header("Руль")]
    public Transform steeringWheel;

    [Header("Коробка передач")]
    public int current;

    private void Start()
    {
        link1 = GetComponent<MechanicalSystem>();
        current = link1.current;
    }    public float[] gearRatios = { -2.5f, 0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f };

    private float throttleInput;
    private float steeringInput;
    private bool isBraking;

    [Header("Состояние")]
    public bool isPlayerInCar = false;

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

    // Обрабатываем ввод игрока
    void HandleInput()
    {
        throttleInput = Input.GetAxis("Vertical");
        steeringInput = GetSteeringInputFromWheel();
    }
    float GetSteeringInputFromWheel()
    {
        float wheelAngle = steeringWheel.rotation.eulerAngles.y;
        float normalizedSteering = NormalizeSteeringAngle(wheelAngle);

        return normalizedSteering;
    }
    float NormalizeSteeringAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0) angle += 360;
        float maxSteeringWheelAngle = 180f;
        float normalizedAngle = Mathf.Clamp(angle, -maxSteeringWheelAngle, maxSteeringWheelAngle) / maxSteeringWheelAngle;

        return normalizedAngle;
    }
    void MoveVehicle()
    {
        float motorTorque = throttleInput * maxMotorTorque;
        if (current >= 0 && current < gearRatios.Length)
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
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        mesh.position = position;
        mesh.rotation = rotation * Quaternion.Euler(0, 0, 90);
    }

}