using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour
{
    private List<GameObject> spawners;
    public Rect windowRect = new Rect(20, 20, 250, 100);
    public List<string> options;
    public List<string> unitCounts = new List<string>();
    private GUIStyle centeredStyle;
    private int units;

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
        float inc = 20;
        // Get unit type and amount
        List<SpawnPair> states = selected.getStates();
        Dictionary<GameObject, int> availableUnits = new Dictionary<GameObject, int>();
        foreach (SpawnPair sp in states)
        {
            if (availableUnits.ContainsKey(sp.UnitType))
            {
                availableUnits[sp.UnitType] += sp.Amount;
            }
            else
            {
                availableUnits.Add(sp.UnitType, sp.Amount);
            }
        }
        
        GUI.Label(new Rect(10, inc, 160, 20), units.ToString() + " units available", centeredStyle);

        // populate the path names
        spawners = selected.getSpawners();
        options = new List<string>();
        foreach (GameObject obj in spawners)
        {
            options.Add(obj.gameObject.name);
        }

        GUI.Label(new Rect(10, inc, 160, 20), units.ToString() + " units available", centeredStyle);

        // populate the path names
        spawners = selected.getSpawners();
        options = new List<string>();
        foreach (GameObject obj in spawners)
        {
            options.Add(obj.gameObject.name);
        }

        inc += 30;
        if (options.Count > 0)
        {
            for(int i = 0; i<options.Count; i++)
            {
                // Add buttons for spawning units
                if (GUI.Button(new Rect(10, inc, 130, 20), options[i], "Button"))
                {
                    //Issue command here
                    print("You selected: '" + options[i] + "'"); 
                }
                // TextField for user input of # of units
                unitCounts.Add(""); // TODO: Figure whether we want this as a field appended to the individual spawn directions
                unitCounts[i] = GUI.TextField(new Rect(145, inc, 25, 20), unitCounts[i], 25, centeredStyle);
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
            selected.clearStates();
            for (int i = 0; i < unitCounts.Count; i++)
            {
                if (unitCounts[i] == "")
                {
                    continue;
                }
                selected.addState(new SpawnPair(i,
                    Resources.Load("Prefab/Soldier", typeof(GameObject)) as GameObject,
                    int.Parse(unitCounts[i]), selected.Owner));
            }
            print("You successfully saved"); // TOADD: Add text from textbox and name of spawners?
            // TODO: Code for applying changes
        }
        // Reapplying size of menu
        windowRect.height = inc + 25;
        windowRect.width = 180;
        windowRect.position = new Vector2(20, 20); // TOADD: Modify when we know the position of the city and make it appear above the city
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
}