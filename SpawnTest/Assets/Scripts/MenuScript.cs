using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour
{
    public Rect windowRect = new Rect(20, 20, 120, 50);
    public List<string> options;
    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, WindowFunction, "");
    }

    public void RecieveList(List<string> input)
    {
        options = input;
    }
    void WindowFunction(int windowID)
    {
        if(options.Count>0)
            foreach (string element in options)
            {
                if (GUI.Button(new Rect(10, 20, 100, 20), element))
                    print("You selected: '" + element + "'"); //Issue command here
            }
        if (GUI.Button(new Rect(10, 20, 100, 20), "Close", "button"))
        {
            print("You closed the window");
            Destroy(gameObject);
        }

        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
}