using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CarEnterExitVR : MonoBehaviour
{
    public Transform driverSeat;
    public GameObject car;
    public GameObject playerRig;
    public GameObject playerModel;
    
    private bool isInCar = false;
    private CarControl carController;

    public GameObject exPoint;

    void Start()
    {
        carController = car.GetComponent<CarControl>();
    }

    public void EnterCar()
    {
        if (!isInCar)
        {
            playerRig.transform.position = driverSeat.position;
            playerRig.transform.rotation = driverSeat.rotation;
            playerRig.GetComponent<CharacterController>().enabled = false;
            isInCar = true;
            carController.isPlayerInCar = true;
            
            if (playerModel != null)
                playerModel.SetActive(false);
        }
    }

    public void ExitCar()
    {
        if (isInCar)
        {
            playerRig.transform.position = exPoint.transform.position;
            playerRig.GetComponent<CharacterController>().enabled = true;
            isInCar = false;
            carController.isPlayerInCar = false;
            
            if (playerModel != null)
                playerModel.SetActive(true);
        }
    }
}
