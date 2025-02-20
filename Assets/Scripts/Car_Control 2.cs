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
    public float maxMotorTorque = 1500f; // Мощность двигателя
    public float maxSteeringAngle = 30f; // Максимальный угол поворота
    public float brakeForce = 3000f; // Сила торможения

    private float throttleInput; // Газ
    private float steeringInput; // Поворот
    private bool isBraking; // Торможение

    [Header("Состояние")]
    public bool isPlayerInCar = false; // Флаг, находится ли игрок в машине

    public int current = 1; // Текущая передача
    public float[] gearRatios = { 0, -2.5f, 0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f };
    private void Update()
    {

        if (isPlayerInCar) // Управление работает только когда игрок в машине
        {
            HandleInput();
            UpdateWheelTransforms();
        }
    }

    private void FixedUpdate()
    {
        /*current = link1.current11;*/
        if (isPlayerInCar)
        {
            MoveVehicle();
            ApplyBrakes();
        }
    }

    // Обрабатываем ввод игрока
    void HandleInput()
    {
        throttleInput = Input.GetAxis("Vertical"); // Газ / тормоз
        isBraking = Input.GetKey(KeyCode.Space); // Тормоз (пробел)
    }

    // Движение автомобиля
    void MoveVehicle()
    {
        float motorTorque = throttleInput * maxMotorTorque;
        rearLeftWheel.motorTorque = motorTorque * gearRatios[current];
        rearRightWheel.motorTorque = motorTorque * gearRatios[current];
    }

    // Торможение
    void ApplyBrakes()
    {
        float appliedBrake = isBraking ? brakeForce : 0f;
        frontLeftWheel.brakeTorque = appliedBrake;
        frontRightWheel.brakeTorque = appliedBrake;
        rearLeftWheel.brakeTorque = appliedBrake;
        rearRightWheel.brakeTorque = appliedBrake;
    }

    // Обновление позиций и вращений моделей колёс
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
    
    // ⚠️ Исправляем поворот колеса, если модель повернута
    mesh.position = position;
    mesh.rotation = rotation * Quaternion.Euler(0, 0, 90);  // Попробуй поменять угол (90, -90, 0)
    }
}
