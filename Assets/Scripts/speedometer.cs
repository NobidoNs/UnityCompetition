using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody carRigidbody; // Ссылка на Rigidbody машины
    public Text speedText; // Ссылка на UI-Text (или TextMeshProUGUI)
    
    void Update()
    {
        if (carRigidbody != null && speedText != null)
        {
            float speed = carRigidbody.velocity.magnitude * 3.6f; // Преобразуем м/с в км/ч
            speedText.text = Mathf.Round(speed) + " km/h"; // Округляем и выводим
        }
    }
}
