using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MenuShop : MonoBehaviour {
    // GUI stuff
    public Rect windowRect = new Rect(20, 20, 250, 100);
    private GUIStyle centeredStyle;

    // Current SpawnPoint and units available for purchase
    private SpawnPoint selected;

    // Unit lists and a maximum purchaseable units
    private List<UnitType> availableUnits = new List<UnitType>();
    private List<int?> units = new List<int?>();
    private int path = 0;
    // Global maximum units to be purchased
    private int maximum = 5;

    public GameObject CastleMenu;

    void OnGUI()
    {
        windowRect = GUI.Window(2, windowRect, WindowFunction, "Menu");
        centeredStyle = GUI.skin.GetStyle("TextField");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
    }

    /// <summary>
    /// Function for selecting SpawnPoint
    /// </summary>
    /// <param name="point">Spawn point for which units will be bought and resources substracted</param>
    public void SetSpawnPoint(SpawnPoint point)
    {
        selected = point; 
        List<SpawnPair> states = selected.getStates();
        foreach (SpawnPair sp in states)
        {
            if (!availableUnits.Contains(sp.UnitType))
                availableUnits.Add(sp.UnitType);
        }
    }

    void WindowFunction(int windowID)
    {
        float inc = 20; // Hello, I am the helpful float who add spacing (vertically) to the menu!
        int temp;

        if (availableUnits.Count > 0)
        {
            for (int i = 0; i < availableUnits.Count; i++)
            {
                // Add labels for each location
                GUI.Label(new Rect(10, inc, 125, 20), availableUnits[i].Name, centeredStyle);
                // TextField for user input of # of units
                string text = "";
                try
                {
                    text = GUI.TextField(new Rect(140, inc, 30, 20), units[i].ToString(), 3, centeredStyle);
                }
                catch (Exception)
                {
                    units.Add(null);
                    text = GUI.TextField(new Rect(140, inc, 30, 20), units[i].ToString(), 3, centeredStyle);
                }

                // Check that input is int only AND ensure unit count never to exceed maximum available
                if (int.TryParse(text, out temp))
                {
                    units[i] = Mathf.Clamp(temp, 0, maximum);
                }
                else if (text == "") units[i] = null;
                inc += 25;
            }
            inc += 5;
        }

        GUI.Label(new Rect(10, inc, 125, 20), "Choose a path: ", centeredStyle);
        // Text field for defining path
        string txt = "";
        txt = GUI.TextField(new Rect(140, inc, 30, 20), path.ToString(), 3, centeredStyle);
        if (int.TryParse(txt, out temp))
        {
            path = Mathf.Clamp(temp, 0, availableUnits.Count);
        }
        else if (txt == "") path = 0;
        inc += 25;

        // Button for closing menu
        if (GUI.Button(new Rect(10, inc, 60, 20), "Close", "Button"))
        {
            close();
        }
        // Button for buying
        if (GUI.Button(new Rect(75, inc, 95, 20), "Buy & close", "Button"))
        {
            for (int i = 0; i < availableUnits.Count; i++)
            {
                if (units[i] == null)
                    units[i] = 0;
                if (units[i] != 0)
                    buyUnit(availableUnits[i], path, (int)units[i]);
            }
            close();
        }
        // Reapplying size of menu
        windowRect.height = inc + 25;
        windowRect.width = 180;
        //windowRect.position = new Vector2(20, 20); // TOADD: Modify when we know the position of the city and make it appear above the city
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    /// <summary>
    /// Function for buying new units
    /// </summary>
    /// <param name="ut">Type of the unit to buy</param>
    /// <param name="path">path for the wave</param>
    /// <param name="amount">amount of units bought</param>
    private void buyUnit(UnitType ut, int path, int amount)
    {
        // checks if there is enough resources
        if (selected.Owner.Resources >= ut.Price * amount)
        {
            selected.Owner.Resources -= ut.Price * amount;
            selected.addState(new SpawnPair(path, ut, amount, selected.Owner));
        }
    }

    /// <summary>
    /// Closes the Window and re-opens CastleMenu
    /// </summary>
    private void close()
    {
        GameObject menu = Instantiate(CastleMenu) as GameObject;
        menu.GetComponent<MenuScript>().SetSpawnPoint(selected);
        print("You closed the window");
        Destroy(gameObject);
    }
}
