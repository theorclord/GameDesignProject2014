using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuNode : MonoBehaviour {

    private CaptureNode captureNode;
    private List<Vector2> pathList;
    public Rect windowRect = new Rect(20, 30, 300, 100);
    public List<int> paths = new List<int>();
    private GUIStyle centeredStyle;
    private GUIStyle buttonPressed;
    private string pathName = "Path ";

    private int selectedLoc;
    private bool activeLoc;
	
	private GUIStyle ButtonStyle = new GUIStyle();
	private GUIStyle SelectedButtonStyle = new GUIStyle();
	
	void OnGUI()
    {
        windowRect = GUI.Window(1, windowRect, WindowFunction, "Menu");
        centeredStyle = GUI.skin.GetStyle("TextField");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
        buttonPressed = GUI.skin.GetStyle("Button");
        buttonPressed.normal.background = GUI.skin.button.active.background;
		ButtonStyle.normal.background = Resources.Load("Sprites/menu_button_normal", typeof(Texture2D)) as Texture2D;
		//SelectedButtonStyle.normal.background = Resources.Load("Sprites/menu_button_dark", typeof(Texture2D)) as Texture2D;
	}
	
    public void setCaptureNode(CaptureNode node)
    {
        captureNode = node;
    }

    void WindowFunction(int windowID)
    {
        float inc = 20; // Hello, I am the helpful float who add spacing (vertically) to the menu!
        GUI.Label(new Rect(10, inc, 160, 40), "Select path for units to follow", centeredStyle);

        inc += 50;

        for (int i= captureNode.TargetPlayer.Count -1; i>-1;i--)
        {
            if (selectedLoc == i && activeLoc)
            {
                if (GUI.Button(new Rect(5, inc, 50, 50), captureNode.TargetPlayer[i].GetComponent<SpriteRenderer>().sprite.texture, ButtonStyle))
                {
                    activeLoc = false;
                    paths.Remove(i);
                    selectedLoc = -1;
                }
            }
            if (GUI.Button(new Rect(5, inc, 50, 50), captureNode.TargetPlayer[i].GetComponent<SpriteRenderer>().sprite.texture))
            {
                selectedLoc = i;
                activeLoc = true;
                paths.Add(i);
                saveFunction();
            }
            inc += 50;
        }

        // Button for closing menu
        if (GUI.Button(new Rect(10, inc, 60, 20), "Close", "Button"))
        {
            GameObject.Find("GameController").GetComponent<GameController>().menuClose();
            Destroy(gameObject);
        }
        // Reapplying size of menu
        windowRect.height = inc + 25;
        windowRect.width = 180;
        windowRect.position = new Vector2(Screen.width - 300, 20); // TOADD: Modify when we know the position of the city and make it appear above the city
        //GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    private void saveFunction()
    {
        captureNode.CustomDirection = paths;
        print("You successfully saved");
    }
}
