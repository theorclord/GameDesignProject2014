using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MenuScript : MonoBehaviour
{
    private List<GameObject> spawners;
    public Rect windowRect = new Rect(20, 20, 250, 100);
    public List<string> options;
    //public List<int?> unitCounts = new List<int?>();
    public List<int?> unitCounts = new List<int?>();
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
        float inc = 20; // Hello, I am the helpful float who add spacing (vertically) to the menu!
        // Get unit type and amount
        List<SpawnPair> states = selected.getStates();
        Dictionary<GameObject, int> availableUnits = new Dictionary<GameObject, int>();
        foreach (SpawnPair sp in states)
        {
            if (availableUnits.ContainsKey(sp.UnitType))
            {
                availableUnits[sp.UnitType] += sp.Amount; // Write each UnitType to a list or use from dictionary (if Amount>0 add)
            }
            else
            {
                availableUnits.Add(sp.UnitType, sp.Amount);
            }
        }
        foreach (KeyValuePair<GameObject, int> kvp in availableUnits)
        {
            units = kvp.Value;
            // break for initial test
            break;
        }
        GUI.Label(new Rect(10, inc, 160, 20), units.ToString() + " units available", centeredStyle);

        inc += 30;

        // populate the path names
        spawners = selected.getSpawners();
        options = new List<string>();
        foreach (GameObject obj in spawners)
        {
            options.Add(obj.gameObject.name);
        }

        if (options.Count > 0)
        {
            for (int i = 0; i < options.Count; i++)
            {
                // Add labels for each location
                GUI.Label(new Rect(10, inc, 130, 20), options[i], centeredStyle);
                inc += 25;
                foreach (var entry in availableUnits)
                {
                    GUI.Label(new Rect(30, inc, 105, 20), entry.Key.GetComponent<Unit>().Name, centeredStyle);
                    // TextField for user input of # of units
                    //unitCounts.Add(null);'
                    string text = "";
                    try
                    {
                        text = GUI.TextField(new Rect(140, inc, 30, 20), unitCounts[i].ToString(), 3, centeredStyle);
                    }
                    catch (Exception)
                    {
                        unitCounts.Add(0);
                        text = GUI.TextField(new Rect(140, inc, 30, 20), unitCounts[i].ToString(), 3, centeredStyle);
                    }

                    // Check that input is int only AND ensure unit count never to exceed maximum available
                    int temp;
                    if (int.TryParse(text, out temp))
                    {
                        int maximumAvailableUnits = units - (int)unitCounts.Sum() + temp; // Change to account for specific type of unit

                        unitCounts[i] = Mathf.Clamp(temp, 0, maximumAvailableUnits);
                    }
                    else if (text == "") unitCounts[i] = null; // Allow the user to delete/clear their input
                    // TODO: Multiple lists, one for each spawn direction + Modify save to account for these lists
                    inc += 25;
                }
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
        if (unitCounts.Sum() < units)
        {
            int minimumValueIndex = unitCounts.IndexOf(unitCounts.Min());
            unitCounts[minimumValueIndex] += (units - unitCounts.Sum());
        }
        for (int i = 0; i < unitCounts.Count; i++)
        {
            if (unitCounts[i] == 0)
            {
                continue;
            }
            selected.addState(new SpawnPair(i,
                Resources.Load("Prefab/Soldier", typeof(GameObject)) as GameObject,
                (int)unitCounts[i], selected.Owner));
        }
        print("You successfully saved");
    }
}