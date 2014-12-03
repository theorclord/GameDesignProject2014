using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGUI : MonoBehaviour {

    // GUI stuff
    public Rect windowRect = new Rect(20, 20, 315, 100);
    private GUIStyle centeredStyle;
    private GUIStyle buttonPressed;

    private GameObject PlayerCastle;

    #region Textures
    // Buy stuff
    public Texture zombie;
    public Texture skeleton;
    public Texture hound;
    public Texture moose;

    // UnitWaves stuff
    private int selectedLoc; 
    private bool activeLoc;
    public Texture mines;
    public Texture woods;
    #endregion

    private SpawnPoint point;

    private int lastPath;
    // Use this for initialization
	void Start () {
        PlayerCastle = GameObject.Find("CastlePlayer");
        point = PlayerCastle.GetComponent<SpawnPoint>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        windowRect = GUI.Window(777, windowRect, WindowFunction, "Menu");
        centeredStyle = GUI.skin.GetStyle("TextField");
        centeredStyle.alignment = TextAnchor.MiddleCenter; 
        buttonPressed = GUI.skin.GetStyle("Button");
        buttonPressed.normal.background = GUI.skin.button.active.background;
    }

    void WindowFunction(int windowID)
    {
        #region Buy Menu EDIT
        GUI.Label(new Rect(10, 10, 310, 20), "Click a unit to add it to your wave!");

        if (point.Owner.Resources < 100)
            GUI.enabled = false;
        if (GUI.Button(new Rect(5, 30, 100, 100), zombie))
            buyUnit("Zombie");
        GUI.Label(new Rect(5, 130, 100, 20), "Zombie 100g");
        if (point.Owner.Resources < 175)
            GUI.enabled = false;
        if (GUI.Button(new Rect(110, 30, 100, 100), skeleton))
            buyUnit("Skeleton Archer");
        GUI.Label(new Rect(100, 130, 100, 20), "Skeleton 175g");
        if (point.Owner.Resources < 250)
            GUI.enabled = false;
        if (GUI.Button(new Rect(215, 30, 100, 100), hound))
            buyUnit("Hound");
        GUI.Label(new Rect(215, 130, 100, 20), "Hounds 250g");
        if (point.Owner.Resources < 800 || !point.Owner.Technology.Contains("Forest"))
            GUI.enabled = false;
        if (GUI.Button(new Rect(85, 155, 150, 150), moose))
            buyUnit("Evil Moose");
        GUI.Label(new Rect(85, 305, 150, 20), "Evil Moose 800g"); 
        GUI.enabled = true;
        #endregion

        #region Locations and UnitWaves
        int vertical = 300;
        int horizontal = 10;
        int spacing = 10; // var in case of Moose
        for (int i = 0; i < point.getSpawners().Count; i++)
        {
            // Check location type
            if (selectedLoc == i && activeLoc)
                if (GUI.Button(new Rect(horizontal, vertical, 50, 50), mines, buttonPressed))
                    activeLoc = false;
            if (GUI.Button(new Rect(horizontal, vertical, 50, 50), mines))
            {
                selectedLoc = i;
                activeLoc = true;
            }
            horizontal += 55;
            spacing += 55;
            foreach (SpawnPair sp in point.getStates())
            {

                #region Check unit type and assign action
                if (sp.Path == i)
                {
                    for (int j = 0; j< sp.Amount; j++)
                    {
                        if (GUI.Button(new Rect(horizontal, vertical, 25, 25), Resources.Load("Sprites/Head" + sp.UnitType.Name, typeof(Texture)) as Texture))
                            UnitClick(selectedLoc, sp.UnitType);
                        horizontal += 27;
                        if (horizontal > 285) // 320 (width) - 25 (size) - 10 (spacing)
                        {
                            vertical += 27;
                            horizontal = spacing;
                            // TODO: Add check for 3rd line of units (reset spacing after 2 lines since moose)
                        }
                    }
                }
                #endregion
            }
            #region Post wave logic for next Location
            horizontal = 10; 
            vertical += 55;
            spacing = 10;
            #endregion
        }
        #endregion

        // Reapplying size of menu
        windowRect.height = vertical;
        windowRect.width = 320;
        //GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        windowRect.position = new Vector2(windowRect.position.x, 50.0f);
    }


    private void buyUnit(string unitNames)
    {
        int currentPath = 0;
        if (!activeLoc)
        {
            lastPath++;
            if (lastPath >= PlayerCastle.GetComponent<SpawnPoint>().spawners.Count)
            {
                lastPath = 0;
            }
            currentPath = lastPath;
        }
        else
        {
            currentPath = selectedLoc;
        }
        
        Player own = PlayerCastle.GetComponent<SpawnPoint>().Owner;
        foreach (UnitType ut in own.unitTypeList)
        {
            if(ut.Name == unitNames)
            {
                if (ut.Price <= own.Resources)
                {
                    own.Resources -= ut.Price;
                    PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(currentPath, ut, 1, own));
					GameObject sound = GameObject.Find ("SoundManager");
					sound.GetComponent<SoundScript> ().playSound (98);
                }
                break;
            }
        }
        
    }

    /// <summary>
    /// Function for managing the action tied to the unit
    /// </summary>
    /// <param name="location">location to which the unit belong</param>
    /// <param name="unit">the type of unit</param>
    void UnitClick(int location, UnitType unit)
    {
        Debug.Log("Clicked the button with the " + unit.Name);
    }
}
