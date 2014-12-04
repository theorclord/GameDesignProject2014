using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGUI : MonoBehaviour {

    // GUI stuff
    private Rect windowRect = new Rect(20, 20, 300, 300);
    private GUIStyle centeredStyle;

    private GameObject PlayerCastle;

    // UnitWaves stuff
    private int selectedLoc; 
    private bool activeLoc;

    private int margin = 55;

    // GUI styles
    private GUIStyle WindowStyle = new GUIStyle();
    private GUIStyle ButtonStyle = new GUIStyle();
    private GUIStyle SelectedButtonStyle = new GUIStyle();

    private SpawnPoint point;

    private int lastPath;
    // Use this for initialization
	void Start () {
        PlayerCastle = GameObject.Find("CastlePlayer");
        point = PlayerCastle.GetComponent<SpawnPoint>();
        WindowStyle.normal.background = Resources.Load("Sprites/menu_bg", typeof(Texture2D)) as Texture2D;
        ButtonStyle.normal.background = Resources.Load("Sprites/menu_button_normal", typeof(Texture2D)) as Texture2D;
        SelectedButtonStyle.normal.background = Resources.Load("Sprites/menu_button_dark", typeof(Texture2D)) as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        windowRect = GUI.Window(777, windowRect, WindowFunction, "", WindowStyle);
        centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
    }

    void WindowFunction(int windowID)
    {
        #region Buy Menu
        GUI.Label(new Rect(margin, 100, 310, 20), "Click a unit to add it to your wave!", centeredStyle);
        Player own = PlayerCastle.GetComponent<SpawnPoint>().Owner;
        int hori = margin;
        int vert = 120;
        int size = 100;
        int horiOld = hori;
        foreach (UnitType ut in own.unitTypeList) {
            if (ut.Name == "Evil Moose"){
                horiOld = hori;
                hori = 135;
                vert += 135;
                size = 150;
                if (point.Owner.Resources < ut.Price || !point.Owner.Technology.Contains("Forest"))
                    GUI.enabled = false;
            }
            if(point.Owner.Resources < ut.Price)
                GUI.enabled = false;
            if (GUI.Button(new Rect(hori, vert, size, size), Resources.Load("Sprites/" + ut.Name, typeof(Texture)) as Texture, ButtonStyle))
                buyUnit(ut.Name);
            GUI.Label(new Rect(hori, vert + size, size, 35), ut.Name + "\n" + ut.Price + "g", centeredStyle);
            hori += size + 5;
            if (ut.Name == "Evil Moose")
            {
                hori = horiOld;
                vert -= 135;
                size = 100;
            }
            // Add code here, for more than 3 basic units if you want..
            GUI.enabled = true;
        }
        #endregion

        #region Locations and UnitWaves
        int vertical = 450;
        int horizontal = margin;
        int spacing = margin; // Var to offset from horizontal w. Location
        for (int i = point.getSpawners().Count - 1; i > -1; i--)
        {
			string arrowLoc = "";
			if(i > 0){
				arrowLoc = "Sprites/arrow_level1_up";
			}else{
				arrowLoc = "Sprites/arrow_level1_down";
			}
            #region Load location and set up button + logic for click
			Texture2D location = (Texture2D) Resources.Load(arrowLoc, typeof(Texture2D)) as Texture2D;
				//point.spawners[i].GetComponent<UnitSpawner>().TargetDirection.GetComponent<SpriteRenderer>().sprite.texture;
            // Location type
            if (selectedLoc == i && activeLoc) {

				if (GUI.Button(new Rect(horizontal, vertical, 50, 50), location,ButtonStyle ))
                    activeLoc = false;
            }
            else {
				if (GUI.Button(new Rect(horizontal, vertical, 50, 50), location, SelectedButtonStyle)) {
                    selectedLoc = i;
                    activeLoc = true;
            } }
            horizontal += margin;
            spacing += margin;
            #endregion
            #region Check unit type and assign action
            foreach (SpawnPair sp in point.getStates())
            {
                if (sp.Path == i)
                {
                    for (int j = 0; j < sp.Amount; j++)
                    {
                        if (horizontal > (420 - 25 - margin)) // full width - icon size - margin
                        {
                            vertical += 27;
                            horizontal = spacing;
                        }
                        if (GUI.Button(new Rect(horizontal, vertical, 25, 25), Resources.Load("Sprites/Head" + sp.UnitType.Name, typeof(Texture)) as Texture, ButtonStyle))
                            UnitClick(selectedLoc, sp.UnitType);
                        horizontal += 27;
                    }
                }
            }
            #endregion
            #region Post wave logic for next Location
            horizontal = margin; 
            vertical += 55;
            spacing = margin;
            #endregion
        }
        #endregion

        // Reapplying size of menu
        windowRect.height = vertical + 75;
        windowRect.width = 420;
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
