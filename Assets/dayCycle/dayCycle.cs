using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public Light moon;
    public Material skyboxDay;
    public Material skyboxNight;
    public GameObject sunObject;
    public GameObject moonObject;
    public float dayDuration = 60f; // Длительность полного цикла дня (в секундах)
    public Gradient lightColor;
    public AnimationCurve lightIntensity;
    
    private float timeElapsed;
    private bool isDay = true;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        float cycleProgress = (timeElapsed % dayDuration) / dayDuration;
        
        // Вращение солнца и луны
        float sunAngle = cycleProgress * 360f - 90f;
        sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
        moon.transform.rotation = Quaternion.Euler(sunAngle + 180f, 170f, 0f);
        
        // Перемещение солнца и луны в небе
        sunObject.transform.position = sun.transform.forward * 100f;
        moonObject.transform.position = moon.transform.forward * 100f;
        
        // Изменение цвета и интенсивности
        sun.color = lightColor.Evaluate(cycleProgress);
        sun.intensity = lightIntensity.Evaluate(cycleProgress);
        moon.intensity = lightIntensity.Evaluate(1 - cycleProgress);
        
        // Смена неба
        if (cycleProgress > 0.5f && isDay)
        {
            RenderSettings.skybox = skyboxNight;
            isDay = false;
        }
        else if (cycleProgress <= 0.5f && !isDay)
        {
            RenderSettings.skybox = skyboxDay;
            isDay = true;
        }
    }
}
