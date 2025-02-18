using UnityEngine;
using UnityEngine.UI;

public class CarRadio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] radioStations;
    public Text stationText;
    
    private int currentStation = 0;

    void Start()
    {

    }

    public void NextStation()
    {
        currentStation = (currentStation + 1) % radioStations.Length;
        PlayStation(currentStation);
    }

    public void PreviousStation()
    {
        currentStation = (currentStation - 1 + radioStations.Length) % radioStations.Length;
        PlayStation(currentStation);
    }

    public void ToggleRadio()
    {
        audioSource.mute = !audioSource.mute;
    }

    private void PlayStation(int index)
    {
        audioSource.clip = radioStations[index];
        audioSource.Play();
        stationText.text = "Station: " + (index + 1);
    }
}
