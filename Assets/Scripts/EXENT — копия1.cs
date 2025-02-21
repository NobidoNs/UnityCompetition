using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CarEnterExitVR : MonoBehaviour
{
    public Transform driverSeat;
    public GameObject car;
    public GameObject playerRig;
    public GameObject playerModel;

    public AudioSource audioSource;
    public AudioClip[] radioStations;

    private bool isInCar = false;
    private Car_Control2 carController;

    public GameObject exPoint;

    void Start()
    {
        carController = car.GetComponent<Car_Control2>();
    }

    public void EnterCar()
    {
        if (!isInCar)
        {
            playerRig.transform.position = driverSeat.position;
            playerRig.transform.rotation = driverSeat.rotation;
            playerRig.GetComponent<LocomotionSystem>().enabled = false;
            isInCar = true;
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
        if (isInCar)
        {
            playerRig.transform.position = exPoint.transform.position;
            playerRig.GetComponent<CharacterController>().enabled = true;
            isInCar = false;
            carController.isPlayerInCar = false;
            
            if (playerModel != null)
                playerModel.SetActive(true);
            audioSource.Stop();
            audioSource.clip = radioStations[1];
            audioSource.Play();
        }
    }
    private void Update()
    {
        if (isInCar)
        {
            playerModel.transform.position = driverSeat.transform.position;
        }
    }
}
