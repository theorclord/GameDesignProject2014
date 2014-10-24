using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MenuPath : MonoBehaviour {
    private List<GameObject> spawners;
    public Rect windowRect = new Rect(20, 20, 250, 100);
    public List<string> options;
    public List<int?> paths = new List<int?>();
    private GUIStyle centeredStyle;

    private SpawnPoint selected;
    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, WindowFunction, "Menu");
        centeredStyle = GUI.skin.GetStyle("TextField");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
    }

    public void setSpawnPoint(SpawnPoint point)
    {
        selected = point;
    }

    void WindowFunction(int windowID)
    {
        float inc = 20; // Hello, I am the helpful float who add spacing (vertically) to the menu!
        GUI.Label(new Rect(10, inc, 160, 20), "Select the % of units for each connected path", centeredStyle);

        inc += 30;

        List<SpawnPair> states = selected.getStates();
        // populate the path names
        spawners = selected.getSpawners();
        options = new List<string>();
        foreach (GameObject obj in spawners)
        {
            options.Add(obj.gameObject.name);
        }

        if (options.Count > 0)
        {
            for(int i = 0; i < options.Count; i++)
            {
                // Add labels for each location
                GUI.Label(new Rect(10, inc, 105, 20), options[i], centeredStyle);
                // TextField for user input of # of units
                string text = "";
                try
                {
                    paths.Add(0);
                } 
                catch(MissingReferenceException) { }
                finally
                {
                    text = GUI.TextField(new Rect(120, inc, 30, 20), paths[i].ToString(), 3, centeredStyle);
                }
                GUI.Label(new Rect(150, inc, 10, 20), "%", centeredStyle);

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
        selected.clearStates();
        if (paths.Sum() < 100)
        {
            int minimumValueIndex = paths.IndexOf(paths.Min());
            paths[minimumValueIndex] += (100 - paths.Sum());
        }
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i] == 0)
            {
                continue;
            }
            selected.addState(new SpawnPair(i,
                Resources.Load("Prefab/Soldier", typeof(GameObject)) as GameObject,
                (int)paths[i], selected.Owner));
        }
        print("You successfully saved");
    }
}

