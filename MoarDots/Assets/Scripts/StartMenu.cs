using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
    public Camera StartScreen;
    public Camera InstructionsScreen;
    public GameObject InstrucText;

    private bool isGUIActive;

    // Use this for initialization
    void Start()
    {
        InstructionsScreen.enabled = false;
        isGUIActive = true;
        TextMesh instructText = InstrucText.GetComponent<TextMesh>();
        instructText.text = "Defeat the Fairy Princess by conquering her castle. \n";
        instructText.text += "Send your units to conquer locations on the map. \n" +
        	"Each wave spawns as indicated by the \"spawntimer\"\n\n";
		instructText.text += "- Mines give you more gold each second\n- Forests give you access to \"the Evil Moose\"!\n";
		instructText.text += "\nZombies are melee fighters\nSkeletal Archers are ranged units\nHounds are fast\nThe Evil" +
			" Moose is EVIL!";
        //instructText.text += "Press 'r' to restart the level, if the ball is still in the game the score resets. \n \n";
        //instructText.text += "By Mikkel Stolborg, based on a concept by Julian Togelius and Marie Gustafsson Friberger";
    }

    void OnGUI()
    {
        // Change the layout of the GUIStyle
        GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
        guiStyle.alignment = TextAnchor.MiddleLeft;

        if (isGUIActive)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 40, 100, 25), "Start game", guiStyle))
            {
                Application.LoadLevel("TestLevel");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 10, 100, 25), "Instructions", guiStyle))
            {
                isGUIActive = false;
                InstructionsScreen.enabled = true;
                StartScreen.enabled = false;
            }
        }
        if (!isGUIActive)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 40.0f, 100, 25), "Return to menu", guiStyle))
            {
                isGUIActive = true;
                InstructionsScreen.enabled = false;
                StartScreen.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
