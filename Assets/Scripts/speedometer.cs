using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public Text speedText;
    
    void Update()
    {
        if (carRigidbody != null && speedText != null)
        {
            float speed = carRigidbody.velocity.magnitude * 3.6f;
            speedText.text = Mathf.Round(speed) + " km/h";
        }
    }
}
