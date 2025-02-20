
using UnityEngine;

public class DoorSoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip enterSound; // ���� ��� �����
    public AudioClip exitSound;  // ���� ��� ������

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
            audioSource.Stop(); // ������������� ������� ���� ����� �������� ������
            audioSource.clip = clip; // ������������� ����� ����
            audioSource.Play();
        }
    }
}