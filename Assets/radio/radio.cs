using UnityEngine;

public class SimpleRadio : MonoBehaviour
{
    public AudioSource audioSource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // Нажатие клавиши R включает/выключает радио
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
            else
                audioSource.Play();
        }
    }
}
