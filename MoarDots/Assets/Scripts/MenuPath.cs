using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MenuPath : MonoBehaviour
{
    private CaptureNode captureNode;
    private List<Vector2> pathList;
    public Rect windowRect = new Rect(20, 20, 250, 100);
    public List<int?> paths = new List<int?>();
    private GUIStyle centeredStyle;
    private string pathName = "Path ";

    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, WindowFunction, "Menu");
        centeredStyle = GUI.skin.GetStyle("TextField");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
    }

    public void setCaptureNode(CaptureNode town)
    {
        captureNode = town;
    }

    void WindowFunction(int windowID)
    {
        float inc = 20; // Hello, I am the helpful float who add spacing (vertically) to the menu!
        GUI.Label(new Rect(10, inc, 160, 40), "Select the % of units\nfor each connected path", centeredStyle);

        inc += 50;

        // populate the path names
        pathList = captureNode.DirectionPlayer;

        if (pathList.Count > 0)
        {
            for (int i = 0; i < pathList.Count; i++)
            {
                // Add labels for each location
                GUI.Label(new Rect(10, inc, 105, 20), pathName + i.ToString(), centeredStyle);
                // TextField for user input of # of units
                string text = "";
                try
                {
                    paths.Add(0);
                }
                catch (MissingReferenceException) { }
                finally
                {
                    text = GUI.TextField(new Rect(120, inc, 30, 20), paths[i].ToString(), 3, centeredStyle);
                }
                GUI.Label(new Rect(150, inc, 20, 20), "%", centeredStyle);

                // Check that input is int only AND ensure unit count never to exceed maximum available
                int temp;
                if (int.TryParse(text, out temp))
                {
                    int maximumAvailableUnits = 100 - (int)paths.Sum() + temp;
                    paths[i] = Mathf.Clamp(temp, 0, 100);
                }
                else if (text == "") paths[i] = null;
                inc += 25;
            }
            inc += 5;
        }
        // Button for closing menu
        if (GUI.Button(new Rect(10, inc, 60, 20), "Close", "Button"))
        {
            print("You closed the window");
            Destroy(gameObject);
        }
        // Button for saving
        if (GUI.Button(new Rect(110, inc, 60, 20), "Save", "Button"))
        {
            SaveFunction();
        }
        // Reapplying size of menu
        windowRect.height = inc + 25;
        windowRect.width = 180;
        //windowRect.position = new Vector2(20, 20); // TOADD: Modify when we know the position of the city and make it appear above the city
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    void SaveFunction()
    {
        if (paths.Sum() <= 0)
        {
            for (int i = 0; i < pathList.Count; i++)
            {
                paths[i] = 100 / pathList.Count;
            }
        } else if (paths.Sum() < 100)
        {
            int minimumValueIndex = paths.IndexOf(paths.Min());
            paths[minimumValueIndex] += (100 - paths.Sum());
        }
        else if (paths.Sum() > 100)
        {
            int maximumValueIndex = paths.IndexOf(paths.Max());
            paths[maximumValueIndex] = 0;
            paths[maximumValueIndex] = (100 - paths.Sum());
        }
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i] == 0)
            {
                continue;
            }
            captureNode.customDirection = paths;
        }
        print("You successfully saved");
    }
}

