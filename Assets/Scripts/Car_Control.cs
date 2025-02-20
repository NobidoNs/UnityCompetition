using UnityEngine;

public class CarControl : MonoBehaviour
{
    // Ссылка на систему коробки передач
    private MechanicalSystem link1;

    // Коллайдеры колёс
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    // Модели колёс
    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    // Параметры автомобиля
    public float maxMotorTorque = 1500f; // Максимальный крутящий момент
    public float brakeForce = 3000f; // Сила торможения
    public bool isPlayerInCar = false; // Флаг, находится ли игрок в машине

    // Коробка передач
    public int current; // Текущая передача
    public float[] gearRatios = { -2.5f, 0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f }; // Передаточные числа

    // Ввод игрока
    private float throttleInput; // Газ/тормоз
    private bool isBraking; // Торможение

    private void Start()
    {
        // Получаем ссылку на систему коробки передач
        link1 = GetComponent<MechanicalSystem>();

        // Инициализация колёс
        UpdateWheelTransforms();
    }

    private void Update()
    {
        // Получаем текущую передачу из системы коробки передач
        current = link1.current;

        // Обрабатываем ввод игрока, если игрок в машине
        if (isPlayerInCar)
        {
            HandleInput();
        }
    }

    private void FixedUpdate()
    {
        // Управляем машиной, если игрок в машине
        if (isPlayerInCar)
        {
            MoveVehicle();
            ApplyBrakes();
            UpdateWheelTransforms();
        }
    }

    // Обработка ввода игрока
    void HandleInput()
    {
        throttleInput = Input.GetAxis("Vertical"); // Газ/тормоз (W/S или стрелки вверх/вниз)
        isBraking = Input.GetKey(KeyCode.Space); // Тормоз (пробел)
    }

    // Движение автомобиля
    void MoveVehicle()
    {
        // Вычисляем крутящий момент на основе ввода и передаточного числа
        float motorTorque = throttleInput * maxMotorTorque;

        // Применяем передаточное число в зависимости от текущей передачи
        if (current >= 0 && current < gearRatios.Length)
        {
            motorTorque *= gearRatios[current];
        }

        // Задняя передача
        if (current == 0)
        {
            rearLeftWheel.motorTorque = -motorTorque;
            rearRightWheel.motorTorque = -motorTorque;
        }
        // Нейтральная передача
        else if (current == 1)
        {
            rearLeftWheel.motorTorque = 0;
            rearRightWheel.motorTorque = 0;
        }
        // Передние передачи
        else if (current >= 2)
        {
            rearLeftWheel.motorTorque = motorTorque;
            rearRightWheel.motorTorque = motorTorque;
        }
    }

    // Применение тормозов
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

    // Обновление позиции и вращения одного колеса
    void UpdateWheel(WheelCollider collider, Transform mesh)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        // Применяем позицию и вращение к модели колеса
        mesh.position = position;
        mesh.rotation = rotation * Quaternion.Euler(0, 0, 90); // Корректировка вращения
    }
}