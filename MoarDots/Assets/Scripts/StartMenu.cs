using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
    public Camera StartScreen;
    public Camera InstructionsScreen;
	public Camera CreditsScreen;
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
		GUIStyle guiStyleNormal = new GUIStyle(GUI.skin.button);
		GUIStyle guiStyleIntro = new GUIStyle(GUI.skin.button);
		GUIStyle guiStyleCredits = new GUIStyle(GUI.skin.button);
        guiStyle.alignment = TextAnchor.MiddleLeft;
		Texture2D startButtonImg = Resources.Load ("Sprites/ButtonStart",typeof(Texture2D)) as Texture2D;
		Texture2D creditsButtonImg = Resources.Load ("Sprites/ButtonCredits",typeof(Texture2D)) as Texture2D;
		Texture2D instructionsButtonImg = Resources.Load ("Sprites/ButtonIntroduction",typeof(Texture2D)) as Texture2D;
		Texture2D backgroundImage = Resources.Load("Sprites/TitleScreenBG",typeof(Texture2D)) as Texture2D;
		Texture2D backgroundSprites = Resources.Load("Sprites/TitleScreen",typeof(Texture2D)) as Texture2D;

		guiStyle.normal.background = startButtonImg;
		guiStyle.hover.background = startButtonImg;

		guiStyleIntro.normal.background = instructionsButtonImg;
		guiStyleIntro.hover.background = instructionsButtonImg;

		guiStyleCredits.normal.background = creditsButtonImg;
		guiStyleCredits.normal.background = creditsButtonImg;
		//transform.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/TitleScreen", typeof(Sprite)) as Sprite;
		//guiStyle.active.background = (Texture2D)Resources.Load("Sprites/TitleScreen", typeof(Texture2D)) as Texture2D;
		//guiStyle.background = (Texture2D)Resources.Load("Sprites/TitleScreen", typeof(Sprite)) as Texture2D;

        if (isGUIActive)
        {
			//InstructionsScreen.gameObject.guiTexture = Resources.Load("Sprites/TitleScreen",typeof(Texture2D)) as Texture2D;

			Rect bgRect = new Rect(0,0,Screen.width,Screen.height);//(Screen.width - backgroundImage.width)/2, (Screen.height - backgroundImage.height)/2,(2*Screen.width) - backgroundImage.width, (2*Screen.height) - backgroundImage.height);
			Rect bgSpriteRect = new Rect((Screen.width - backgroundSprites.width) / 2,(Screen.height - backgroundSprites.height) / 2, backgroundSprites.width, backgroundSprites.height);
			GUIStyle bgSt = new GUIStyle(GUI.skin.box);
			GUIStyle bgSpriteSt = new GUIStyle(GUI.skin.box);
			bgSpriteSt.normal.background = backgroundSprites;
			bgSt.normal.background = backgroundImage;
			GUI.Label(bgRect,"",bgSt);
			GUI.Label(bgSpriteRect,"",bgSpriteSt);
			if (GUI.Button(new Rect((Screen.width/ 2)- (startButtonImg.width/2),((Screen.height - backgroundSprites.height) / 2) - 120, startButtonImg.width/*100*/,startButtonImg.height /*25*/), "", guiStyle))
            {
                Application.LoadLevel("TestLevel");
            }
            if (GUI.Button(new Rect((Screen.width / 2) - (instructionsButtonImg.width/2), ((Screen.height - backgroundSprites.height) / 2) - 75, instructionsButtonImg.width, instructionsButtonImg.height), "", guiStyleIntro))
            {
                isGUIActive = false;
                InstructionsScreen.enabled = true;
                StartScreen.enabled = false;
				CreditsScreen.enabled = false;
            }
			if(GUI.Button(new Rect((Screen.width/2)-(creditsButtonImg.width/2),((Screen.height - backgroundSprites.height)/2)-30, creditsButtonImg.width,creditsButtonImg.height),"", guiStyleCredits))
			{
				isGUIActive = false;
				InstructionsScreen.enabled = false;
				StartScreen.enabled= false;
				CreditsScreen.enabled = false;
			}
			/* if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.Height/2 +30, 100, 20), "", guiStyle))
			 * {
			 * 	isGUIActive = false;
			 * 	InstructionsScreen.enabled = false;
			 * 	StartScreen.enabled = false;
			 * 	CreditsScreen.enabled = true;
			 * }
			 */
        }
        if (!isGUIActive)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 40.0f, 100, 25), "Return to menu", guiStyleNormal))
            {
                isGUIActive = true;
                InstructionsScreen.enabled = false;
				CreditsScreen.enabled = false;
                StartScreen.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
