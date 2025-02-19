using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
public class MechanicalSystem : MonoBehaviour
{

    private ScriptableObject pe;
    public int current = -1;
    public Object Richag; 
    public Object line1;
    public Object line2;
    public Object line3;
    public Object line4;
    public Object line5;
    public Object line6;
    public Object line7;
    void Update()
    {
        double mini = Mathf.Min(Mathf.Max(((GameObject)line1).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line1).transform.position.x, ((GameObject)Richag).transform.position.x), Mathf.Max(((GameObject)line2).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line2).transform.position.x, ((GameObject)Richag).transform.position.x), Mathf.Max(((GameObject)line3).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line3).transform.position.x, ((GameObject)Richag).transform.position.x), Mathf.Max(((GameObject)line4).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line4).transform.position.x, ((GameObject)Richag).transform.position.x), Mathf.Max(((GameObject)line5).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line5).transform.position.x, ((GameObject)Richag).transform.position.x), Mathf.Max(((GameObject)line6).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line7).transform.position.x, ((GameObject)Richag).transform.position.x), Mathf.Max(((GameObject)line1).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line7).transform.position.x, ((GameObject)Richag).transform.position.x));
        if (Mathf.Max(((GameObject)line1).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line1).transform.position.x, ((GameObject)Richag).transform.position.x) == mini){
            ((GameObject)Richag).transform.position = new Vector3(((GameObject)line1).transform.position.x, ((GameObject)Richag).transform.position.y, ((GameObject)Richag).transform.position.z);
            current = 1;
        }else if(Mathf.Max(((GameObject)line2).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line2).transform.position.x, ((GameObject)Richag).transform.position.x) == mini)
        {
            ((GameObject)Richag).transform.position = new Vector3(((GameObject)line2).transform.position.x, ((GameObject)Richag).transform.position.y, ((GameObject)Richag).transform.position.z);
            current = 2;
        }else if(Mathf.Max(((GameObject)line3).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line3).transform.position.x, ((GameObject)Richag).transform.position.x) == mini)
        {
            ((GameObject)Richag).transform.position = new Vector3(((GameObject)line3).transform.position.x, ((GameObject)Richag).transform.position.y, ((GameObject)Richag).transform.position.z);
            current = 3;
        }else if(Mathf.Max(((GameObject)line4).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line4).transform.position.x, ((GameObject)Richag).transform.position.x) == mini)
        {
            ((GameObject)Richag).transform.position = new Vector3(((GameObject)line4).transform.position.x, ((GameObject)Richag).transform.position.y, ((GameObject)Richag).transform.position.z);
            current = 4;
        }else if(Mathf.Max(((GameObject)line5).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line5).transform.position.x, ((GameObject)Richag).transform.position.x) == mini)
        {
            ((GameObject)Richag).transform.position = new Vector3(((GameObject)line5).transform.position.x, ((GameObject)Richag).transform.position.y, ((GameObject)Richag).transform.position.z);
            current = 5;
        }else if(Mathf.Max(((GameObject)line6).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line6).transform.position.x, ((GameObject)Richag).transform.position.x) == mini)
        {
            ((GameObject)Richag).transform.position = new Vector3(((GameObject)line6).transform.position.x, ((GameObject)Richag).transform.position.y, ((GameObject)Richag).transform.position.z);
            current = 6;
        }else if(Mathf.Max(((GameObject)line7).transform.position.x, ((GameObject)Richag).transform.position.x) - Mathf.Min(((GameObject)line7).transform.position.x, ((GameObject)Richag).transform.position.x) == mini)
        {
            ((GameObject)Richag).transform.position = new Vector3(((GameObject)line7).transform.position.x, ((GameObject)Richag).transform.position.y, ((GameObject)Richag).transform.position.z);
            current = 7;
        }
    }
}
