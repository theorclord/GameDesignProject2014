using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialController : MonoBehaviour {

    private float GoldTimer = 5;
    public float SpawnTimer = 15;
    public GameObject nodeMenu;
    private bool nodeOpen;

    public GameObject PlayerCastle;
    public GameObject EnemyCastle;

    private Player player;
    private Player enemy;
    private Player neutral;

    private List<Player> playerList = new List<Player>();
    //global timer
    private float interval = 1.0f;
    private float timer = 0.0f;
    private float startTime;

    // Resource income checker
    private bool goldIncome = false;
    // Use this for initialization

    public bool Mine1stCaptured = false;
    public bool Forest1stCaptured = false;
    public GameObject TheForest;
    public GameObject TheMine;
    public DarkenedScreen blackenedScreen;
    public CaptureMessages messages;

    private int ButtonPressed = 0;

    void Start()
    {
        startTime = Time.time;
        //Timer
        timer = Time.time + interval - startTime;

        #region UnitAndPlayInitialization
        //initialize unit types
        // string name, int atk, float health, int range, int movespeed, bool isRanged, int price, bool isStructure
        //,float armour, float armourPen, float attackSpeed, unit tier
        UnitType soldierType = new UnitType("Soldier", 10f, 50f, 100, 15, false, 100, false, 0, 0, 1, UnitType.LOWER_TIER, "None");
        UnitType skeletonType = new UnitType("Zombie", 10, 50f, 100, 15, false, 100, false, 0, 0, 1, UnitType.LOWER_TIER, "None");
        //UnitType fairyType = new UnitType("Fairy", 7f, 25f, 600, 12, true, 175, false, 0, 0f, 1, UnitType.LOWER_TIER, "None");
        //UnitType skeletonArcherType = new UnitType("Skeleton Archer", 7f, 25f, 600, 12, true, 175, false, 0, 0f, 1, UnitType.LOWER_TIER, "None");
        //UnitType evilMooseType = new UnitType("Evil Moose", 15f, 400, 100, 10, false, 800, false, 5, 0, 1, UnitType.HIGHER_TIER, "Forest");
        //UnitType unicornType = new UnitType("Unicorn", 15f, 400, 100, 10, false, 800, false, 5, 0, 1, UnitType.HIGHER_TIER, "Forest");
        //UnitType houndType = new UnitType("Hound", 10, 80, 100, 30, false, 250, false, 0, 0, 1, UnitType.MIDDLE_TIER, "None");

        //Initialize players
        //Ai
        enemy = new Player();
        enemy.playerColor = Color.red;
        enemy.Name = "enemy";
        enemy.unitTypeList.Add(soldierType);
        //enemy.unitTypeList.Add(fairyType);
        //enemy.unitTypeList.Add(unicornType);
        enemy.Income = 50;
        enemy.Resources = 1000;
        enemy.Technology.Add("None");
        GameObject.Find("AIController").GetComponent<TutorialAI>().SetAI(enemy);
        playerList.Add(enemy);

        //Player
        player = new Player();
        player.playerColor = Color.cyan;
        player.Name = "player";
        player.unitTypeList.Add(skeletonType);
        //player.unitTypeList.Add(skeletonArcherType);
        //player.unitTypeList.Add(evilMooseType);
        //player.unitTypeList.Add(houndType);

        player.Income = 50;
        player.Resources = 5000;
        player.Technology.Add("None");

        playerList.Add(player);

        //Neutral
        // should be used for initial control
        neutral = new Player();
        neutral.playerColor = Color.gray;
        neutral.Name = "neutral";

        playerList.Add(neutral);

        //Setup basic spawn
        PlayerCastle.GetComponent<TutorialSpawnPoint>().Owner = player;
        PlayerCastle.GetComponent<Castle>().Owner = player;
        PlayerCastle.GetComponent<TutorialSpawnPoint>().addState(new SpawnPair(0, player.unitTypeList[0], 1, player));
        PlayerCastle.GetComponent<TutorialSpawnPoint>().addState(new SpawnPair(1, player.unitTypeList[0], 1, player));

        EnemyCastle.GetComponent<TutorialSpawnPoint>().Owner = enemy;
        EnemyCastle.GetComponent<Castle>().Owner = enemy;
        EnemyCastle.GetComponent<TutorialSpawnPoint>().addState(new SpawnPair(0, enemy.unitTypeList[0], 0, enemy));
        EnemyCastle.GetComponent<TutorialSpawnPoint>().addState(new SpawnPair(1, enemy.unitTypeList[0], 0, enemy));
        #endregion

        TheForest = GameObject.Find("forest");
        TheMine = GameObject.Find("gold_mine");
        blackenedScreen = GameObject.Find("blackened_screen").GetComponent<DarkenedScreen>();
        messages = GameObject.Find("capture_messages").GetComponent<CaptureMessages>();

    }

    void OnGUI()
    {
        Debug.Log("Before Everything.");
        Event e = Event.current;
 //       if (e.isKey && e.keyCode == KeyCode.Space)
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("The if is working!!!");
            blackenedScreen.DisableSprite();
//            GameObject.Find("blackened_screen").GetComponent<SpriteRenderer>().enabled = false;
//            GameObject.Find("blackened_screen").GetComponent<SpriteRenderer>().sprite = null;
            Debug.Log("Black screen sprite is: " + GameObject.Find("blackened_screen").GetComponent<SpriteRenderer>().sprite +
                " and Enabled is: " + GameObject.Find("blackened_screen").GetComponent<SpriteRenderer>().enabled);

            messages.HideMsg();
//            GameObject.Find("capture_messages").GetComponent<SpriteRenderer>().enabled = false;
//            GameObject.Find("capture_messages").GetComponent<SpriteRenderer>().sprite = null;
            Debug.Log("Message sprite is: " + GameObject.Find("capture_messages").GetComponent<SpriteRenderer>().sprite +
                " and Enabled is: " + GameObject.Find("capture_messages").GetComponent<SpriteRenderer>().enabled);

//            Time.timeScale = 1;
            unpause();
            Debug.Log("Time Scale is : " + Time.timeScale);

            ButtonPressed++;
            Debug.Log("Button is pressed : " + ButtonPressed + " times.");

        }

    }

    void Update()
    {
        // Update player resource display
        //TODO if more players should have a generic player lookup
        GameObject.Find("PlayerResources").guiText.text = "Gold: " + playerList[1].Resources;
        #region GlobalTimer
        //Global timer update
        if (Time.time >= timer + startTime)
        {
            timer += interval;
            goldIncome = true;
        }
        if (timer >= 60)
        {

            string min = "";
            if (Mathf.Floor(timer / 60) > 0)
            {
                min = "0" + Mathf.Floor(timer / 60);
            }
            else if (Mathf.Floor(timer / 60) > 9)
            {
                min = "" + Mathf.Floor(timer / 60);
            }
            string sec = "";
            if (timer % 60 < 10)
            {
                sec = "0" + timer % 60;
            }
            else
            {
                sec = "" + timer % 60;
            }
            GameObject.Find("Timer").guiText.text = "Timer: " + min + ":" + sec;
        }
        else
        {
            string str = "";
            if (timer % 60 < 10)
            {
                str = "0" + timer % 60;
            }
            else
            {
                str = "" + timer % 60;
            }
            GameObject.Find("Timer").guiText.text = "Timer: " + "00:" + str;

        }
        #endregion

        //next spawnTimer
        GameObject.Find("SpawnTimer").guiText.text = "Time till next spawn: " + (SpawnTimer - (timer % SpawnTimer));

        //update gold
        if ((int)timer % GoldTimer == 0 && goldIncome)
        {
            foreach (Player play in playerList)
            {
                play.Resources += play.Income;
            }
            goldIncome = false;
        }

        //Gets the postion of the mouse on camera
        Vector3 camCoord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(camCoord.x, camCoord.y);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, new Vector2(0.0f, 0.0f));
            if (hit)
            {
                //Debug.Log("Hit " + hit.transform.gameObject.name);
                // TODO: Add external check if menu is open, and close it!
                if (hit.transform.gameObject.tag == "CaptureNode" && !nodeOpen)// && hit.transform.gameObject.GetComponent<CaptureNode>().Owner == playerList[1]) //TODO fix for selecting player
                {
                    GameObject menu = Instantiate(nodeMenu) as GameObject;
                    menu.GetComponent<MenuNode>().setCaptureNode(hit.transform.gameObject.GetComponent<CaptureNode>());
                    nodeOpen = true;
                }
                else
                {
                    Debug.Log("Non selectable");
                }
            }
        }

        checkLocations();
    }

    /// <summary>
    /// gets the global timer
    /// </summary>
    /// <returns>time in floating value</returns>
    public float GetTimer()
    {
        return timer;
    }

    public void menuClose()
    {
        nodeOpen = false;
    }

    private void pause()
    {
        Time.timeScale = 0;
    }

    private void unpause()
    {
        Time.timeScale = 1;
    }

    //♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥
    //♥♥♥ check if mine or forest is captured ♥♥♥
    //♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥

    public void checkLocations()
    {
        if (TheMine.GetComponent<CaptureNode>().Owner != null)
        {            
                if (TheMine.GetComponent<CaptureNode>().Owner.Name.Equals("player") && Mine1stCaptured == false)
                {
                    pause();
                    blackenedScreen.EnableSprite();
                    messages.ShowMineMsg();

                }
            }

            if (TheForest.GetComponent<CaptureNode>().Owner != null)
            {

                if (TheForest.GetComponent<CaptureNode>().Owner.Name.Equals("player") && Forest1stCaptured == false)
                {
                    pause();
                    blackenedScreen.EnableSprite();
                    messages.ShowForestMsg();
                }
            }
        
    }
}
