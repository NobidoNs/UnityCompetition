using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Поворотколес : MonoBehaviour
{
    public GameObject LeftWheel;
    public GameObject RightWheel;
    public GameObject SteeringWheel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {   
        Vector3 steeringRotation = SteeringWheel.transform.rotation.eulerAngles;
        Vector3 leftWheelRotation = LeftWheel.transform.rotation.eulerAngles;
        leftWheelRotation.y = steeringRotation.x / 24f;
        LeftWheel.transform.rotation = Quaternion.Euler(leftWheelRotation);

        Vector3 rightWheelRotation = RightWheel.transform.rotation.eulerAngles;
        rightWheelRotation.y = steeringRotation.x / 24f;
        RightWheel.transform.rotation = Quaternion.Euler(rightWheelRotation);
    }
}
