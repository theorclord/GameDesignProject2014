using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour
{
    private List<GameObject> spawners;
    public Rect windowRect = new Rect(20, 20, 250, 100);
    public List<string> options;
    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, WindowFunction, "CastleMenu");
    }

    public void RecieveList(List<GameObject> input)
    {
        spawners = input;
        options = new List<string>();
        foreach (GameObject obj in spawners)
        {
            options.Add(obj.gameObject.name);
        }
    }
    void WindowFunction(int windowID)
    {
        float inc = 20;
        if (options.Count > 0)
        {
            for(int i = 0; i<options.Count; i++)
            {
                if (GUI.Button(new Rect(10, inc, 160, 20), options[i], "button"))
                {
                    spawners[i].GetComponent<UnitSpawner>().addUnits(Resources.Load("Prefab/Soldier", typeof(GameObject)) as GameObject, 1);
                    print("You selected: '" + options[i] + "'"); //Issue command here
                }
                inc += 25;
            }
            inc += 5;
        }
        if (GUI.Button(new Rect(10, inc, 160, 20), "Close", "button"))
        {
            print("You closed the window");
            Destroy(gameObject);
        }
        windowRect.height = inc + 25;
        windowRect.width = 180;
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
}