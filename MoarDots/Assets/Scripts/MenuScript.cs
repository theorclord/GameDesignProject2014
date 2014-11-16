using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MenuScript : MonoBehaviour
{
    // GUI stuff
    private List<GameObject> spawners;
    public Rect windowRect = new Rect(20, 20, 250, 100);
    private GUIStyle centeredStyle;

    public GameObject ShopMenu;

    // List of names for available Paths
    public List<string> options;
    private SpawnPoint selected;

    // List of total available and selected units
    private Dictionary<UnitType, int> availableUnits;
    private Dictionary<UnitType, List<int?>> unitQueues = new Dictionary<UnitType, List<int?>>();

    private string pathName = "Path ";

    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, WindowFunction, "Menu");
        centeredStyle = GUI.skin.GetStyle("TextField");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
    }

    /// <summary>
    /// Function for selecting SpawnPoint
    /// </summary>
    /// <param name="point">Spawn point for each wave</param>
    public void SetSpawnPoint(SpawnPoint point)
    {
        selected = point;
    }

    void WindowFunction(int windowID)
    {
        float inc = 20; // A float for adding spacing (vertically) to the menu!
        // Get unit type and amount
        List<SpawnPair> states = selected.getStates();
        availableUnits = new Dictionary<UnitType, int>();
        foreach (SpawnPair sp in states)
        {
            // NICE: Add current values to unitQueues
            if (availableUnits.ContainsKey(sp.UnitType))
            {
                availableUnits[sp.UnitType] += sp.Amount; // Write each UnitType to a list or use from dictionary (if Amount>0 add)
            }
            else
            {
                availableUnits.Add(sp.UnitType, sp.Amount);
            }

            if (!unitQueues.ContainsKey(sp.UnitType))
            {
                // Add unitTypes to unitQueues
                unitQueues.Add(sp.UnitType, new List<int?>());
            }
        }

        // Write Label telling total available units (and types)
        string units = "Available Units:";
        int siz = 20;
        foreach (KeyValuePair<UnitType, int> kvp in availableUnits)
        {
            units += ("\n" + kvp.Key.Name + ": " + kvp.Value);
            siz += 15;
        }
        GUI.Label(new Rect(10, inc, 160, siz), units, centeredStyle);

        inc += siz + 5;

        // Populate the path names
        spawners = selected.getSpawners();
        options = new List<string>();
        foreach (GameObject obj in spawners)
        {
            options.Add(obj.gameObject.name);
        }
        #region Labels and textboxes for unit assignment
        if (options.Count > 0)
        {
            for (int i = 0; i < options.Count; i++)
            {
                // Add labels for each location
                GUI.Label(new Rect(10, inc, 130, 20), pathName + i.ToString(), centeredStyle);
                inc += 25;
                foreach (KeyValuePair<UnitType, List<int?>> kvp in unitQueues)
                {
                    GUI.Label(new Rect(30, inc, 105, 20), kvp.Key.Name, centeredStyle);
                    // TextField for user input of # of units
                    string text = "";
                    try
                    {
                        text = GUI.TextField(new Rect(140, inc, 30, 20), kvp.Value[i].ToString(), 3, centeredStyle);
                    }
                    catch (Exception)
                    {
                        kvp.Value.Add(null); // set default value; null if textbox should be <blank>
                        text = GUI.TextField(new Rect(140, inc, 30, 20), kvp.Value[i].ToString(), 3, centeredStyle);
                    }

                    // Check that input is int only AND ensure unit count never to exceed maximum available
                    int temp;
                    if (int.TryParse(text, out temp))
                    {
                        int maximumAvailableUnits = availableUnits[kvp.Key] - (int)kvp.Value.Sum() + temp;

                        kvp.Value[i] = Mathf.Clamp(temp, 0, maximumAvailableUnits);
                    }
                    else if (text == "") kvp.Value[i] = null; // Allow the user to delete/clear their input
                    inc += 25;
                }
            }
            inc += 5;
        }
        #endregion

        //Button for buying
        if (GUI.Button(new Rect(10, inc, 160, 20), "Buy stuff", "Button"))
        {
            print("You bought stuff!");
            buyFunction();
        }
        inc += 25;
        // Button for closing menu
        if (GUI.Button(new Rect(10, inc, 60, 20), "Close", "Button"))
        {
            print("You closed the window");
            Destroy(gameObject);
        }
        // Button for saving
        if (GUI.Button(new Rect(110, inc, 60, 20), "Save", "Button"))
        {
            saveFunction();
        }
        // Reapplying size of menu
        windowRect.height = inc + 25;
        windowRect.width = 180;
        //windowRect.position = new Vector2(20, 20); // TOADD: Modify when we know the position of the city and make it appear above the city
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    /// <summary>
    /// Function used by the Buy-button.
    /// Used for initiating a new Window.
    /// NB: Closes current menu!
    /// </summary>
    private void buyFunction()
    {
        GameObject menu = Instantiate(ShopMenu) as GameObject;
        menu.GetComponent<MenuShop>().SetSpawnPoint(selected);
        Destroy(this);
    }

    /// <summary>
    /// Function used by the Save-button.
    /// Checks the textfields and assign the waves.
    /// </summary>
    private void saveFunction()
    {
        List<SpawnPair> currentStates = selected.getStates();

        selected.clearStates();
        foreach (KeyValuePair<UnitType, List<int?>> kvp in unitQueues)
        {
            // Fill in minimum value
            int unitCounts = availableUnits[kvp.Key];
            for(int i = 0; i<kvp.Value.Count;i++)
            {
                if (kvp.Value[i] == null)
                {
                    kvp.Value[i] = 0;
                }
            }
            if (kvp.Value.Sum() < unitCounts)
            {
                int minimumValueIndex = kvp.Value.IndexOf(kvp.Value.Min());
                kvp.Value[minimumValueIndex] += (unitCounts - kvp.Value.Sum());
            }
            for (int i = 0; i < kvp.Value.Count; i++)
            {
                // Send units
                selected.addState(new SpawnPair(i, kvp.Key, (int)kvp.Value[i], selected.Owner));
            }
        }
        print("You successfully saved");
    }
}