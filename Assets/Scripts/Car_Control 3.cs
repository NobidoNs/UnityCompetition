

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Car_Control2 : MonoBehaviour
{
    public GameObject Richag; // Ручка коробки передач
    public GameObject[] lines; // Линии передач 

    private bool isGrabbed = false; // Флаг, указывающий, что ручка захвачена


    public Transform driverSeat;
    public GameObject car;
    public GameObject playerRig;
    public GameObject playerModel;

    public AudioSource audioSource;
    public AudioClip[] radioStations;
    private Car_Control2 carController;

    public GameObject exPoint;

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
            playerRig.transform.position = driverSeat.transform.position;
            accelerateAction.action.Enable();
            brakeAction.action.Enable();
            HandleInput();
            UpdateWheelTransforms();
        }else{
            accelerateAction.action.Disable();
            brakeAction.action.Disable();
        }
        if (!isGrabbed)
        {
            GameObject nearestLine = FindNearestLine();

            if (nearestLine != null)
            {
                Richag.transform.position = new Vector3(
                    Richag.transform.position.x,
                    Richag.transform.position.y,
                    nearestLine.transform.position.z // Используем Z вместо X
                );
                current = System.Array.IndexOf(lines, nearestLine) + 1;
            }
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



    public void EnterCar()
    {
        if (!isPlayerInCar)
        {
            playerRig.transform.position = driverSeat.position; 
            isPlayerInCar = true;
            carController.isPlayerInCar = true;

            if (playerModel != null)
                playerModel.SetActive(false);
            audioSource.Stop();
            audioSource.clip = radioStations[0];
            audioSource.Play();
            Debug.Log(radioStations[0]);
        }
    }

    public void ExitCar()
    {
        if (isPlayerInCar)
        {
            playerRig.transform.position = exPoint.transform.position;
            isPlayerInCar = false;
            carController.isPlayerInCar = false;

            if (playerModel != null)
                playerModel.SetActive(true);
            audioSource.Stop();
            audioSource.clip = radioStations[1];
            audioSource.Play();
        }
    }
  

    // Метод для поиска ближайшей линии
    GameObject FindNearestLine()
    {
        GameObject nearestLine = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject line in lines)
        {
            if (line == null) continue;

            // Вычисляем расстояние по оси Z
            float distance = Mathf.Abs(line.transform.position.z - Richag.transform.position.z);

            // Если расстояние меньше минимального, обновляем ближайшую линию
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestLine = line;
            }
        }

        return nearestLine;
    }

    // Метод для установки состояния "захвачено"
    public void SetGrabbed(bool grabbed)
    {
        isGrabbed = grabbed;
    }
}
