using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Поворотколес : MonoBehaviour
{
    int count = 0;
    public GameObject LeftWheel;
    public GameObject RightWheel;
    public GameObject SteeringWheel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {   
        float steeringRotation = SteeringWheel.transform.rotation.eulerAngles.z;
        if(steeringRotation > 180 && count != 2)
        {
            steeringRotation -= 360;
            count += 1;
        }
        
        else if(steeringRotation > 180)
        {  
            SteeringWheel.transform.rotation = Quaternion.Euler(90, 0, 180);
        }
        
        LeftWheel.transform.rotation = Quaternion.Euler(LeftWheel.transform.rotation.eulerAngles.x, LeftWheel.transform.rotation.eulerAngles.y, steeringRotation);
        RightWheel.transform.rotation = Quaternion.Euler(RightWheel.transform.rotation.eulerAngles.x, RightWheel.transform.rotation.eulerAngles.y, steeringRotation);
        

        /*Vector3 steeringRotation = SteeringWheel.transform.rotation.eulerAngles;
        Vector3 leftWheelRotation = LeftWheel.transform.rotation.eulerAngles;
        leftWheelRotation.y = steeringRotation.x / 24f;
        LeftWheel.transform.rotation = Quaternion.Euler(leftWheelRotation);

        Vector3 rightWheelRotation = RightWheel.transform.rotation.eulerAngles;
        rightWheelRotation.y = steeringRotation.x / 24f;
        RightWheel.transform.rotation = Quaternion.Euler(rightWheelRotation);
        */
    }
}