using UnityEngine;

public class MechanicalSystem : MonoBehaviour
{
    public GameObject Richag;
    public GameObject[] lines;
    public int current = 0;

    void Update()
    {
        GameObject nearestLine = FindNearestLine();
        if (nearestLine != null)
        {
            Richag.transform.position = new Vector3(
                nearestLine.transform.position.x,
                Richag.transform.position.y,
                Richag.transform.position.z
            );
            current = System.Array.IndexOf(lines, nearestLine) + 1;
        }
    }

    GameObject FindNearestLine()
    {
        GameObject nearestLine = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject line in lines)
        {
            if (line == null) continue;
            float distance = Mathf.Abs(line.transform.position.x - Richag.transform.position.x);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestLine = line;
            }
        }
        return nearestLine;
    }
}
