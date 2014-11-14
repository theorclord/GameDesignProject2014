using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour {

    // Tooltips
    public string toolTipText = ""; // set this in the Inspector

    private string currentToolTipText = "";
    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;
    // End tooltip

	// Use this for initialization
	void Start () {
        // Start tooltip
        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.white;
        guiStyleFore.alignment = TextAnchor.UpperCenter;
        guiStyleFore.wordWrap = true; 
        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.black;
        guiStyleBack.alignment = TextAnchor.UpperCenter;
        guiStyleBack.wordWrap = true; 
        // end tooltip
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Start tooltip
    void OnMouseOver()
    {
        currentToolTipText = toolTipText;
    }

    void OnMouseExit()
    {
        currentToolTipText = "";
    }

    void OnGUI()
    {
        if (currentToolTipText != "")
        {
            float x = Event.current.mousePosition.x;
            float y = Event.current.mousePosition.y;
            GUIContent content = new GUIContent(currentToolTipText);
            GUIStyle style = new GUIStyle(GUI.skin.button);
            Vector2 size = style.CalcSize(content);
            float sizex = size.x + 8;
            GUI.Label(new Rect(x - sizex / 2, y - 20, sizex, size.y + 4), currentToolTipText, style);

        }
    }
    //end tooltip
}
