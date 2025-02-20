
using UnityEngine;

public class DoorSoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip enterSound; // Звук при входе
    public AudioClip exitSound;  // Звук при выходе

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaySound(enterSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaySound(exitSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.Stop(); // Останавливаем текущий звук перед запуском нового
            audioSource.clip = clip; // Устанавливаем новый звук
            audioSource.Play();
        }
    }
}