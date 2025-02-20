using UnityEngine;

public class MechanicalSystem : MonoBehaviour
{
    public GameObject Richag;
    public GameObject[] lines;
    public int current11 = 3;

    void Update()
    {
        GameObject nearestLine = FindNearestLine();
        if (nearestLine != null)
        {
            Richag.transform.position = new Vector3(
                Richag.transform.position.x,
                Richag.transform.position.y,
                nearestLine.transform.position.z
            );
            current11 = System.Array.IndexOf(lines, nearestLine) + 1;
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
