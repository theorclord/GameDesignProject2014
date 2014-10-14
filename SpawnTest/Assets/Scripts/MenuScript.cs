using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour
{
    public Rect windowRect = new Rect(20, 20, 250, 100);
    public List<string> options;
    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, WindowFunction, "CastleMenu");
    }

    public void RecieveList(List<string> input)
    {
        options = input;
    }
    void WindowFunction(int windowID)
    {
        float inc = 20;
        if (options.Count > 0)
        {
            foreach (string element in options)
            {
                if (GUI.Button(new Rect(10, inc, 160, 20), element, "button"))
                    print("You selected: '" + element + "'"); //Issue command here
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