using UnityEngine;

public class MechanicalSystem : MonoBehaviour
{
    public GameObject Richag; // Ручка коробки передач
    public GameObject[] lines; // Линии передач
    public int current = 0; // Текущая передача

    private bool isGrabbed = false; // Флаг, указывающий, что ручка захвачена

    void Update()
    {
        // Если ручка не захвачена, перемещаем её к ближайшей линии
        if (!isGrabbed)
        {
            GameObject nearestLine = FindNearestLine();

            if (nearestLine != null)
            {
                Richag.transform.position = new Vector3(
                    nearestLine.transform.position.x + 3.851777f,
                    Richag.transform.position.y,
                    nearestLine.transform.position.z // Используем Z вместо X
                );
                current = System.Array.IndexOf(lines, nearestLine) + 1;
            }
        }
    }

    // Метод для поиска ближайшей линии
    GameObject FindNearestLine()
    {
        GameObject nearestLine = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject line in lines)
        {
            if (line == null) continue;

            // Вычисляем расстояние по оси Z
            float distance = Mathf.Abs(line.transform.position.z - Richag.transform.position.z);

            // Если расстояние меньше минимального, обновляем ближайшую линию
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestLine = line;
            }
        }

        return nearestLine;
    }

    // Метод для установки состояния "захвачено"
    public void SetGrabbed(bool grabbed)
    {
        isGrabbed = grabbed;
    }
}