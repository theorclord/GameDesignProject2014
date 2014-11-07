using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject castleMenu;
    public GameObject townMenu;

    public GameObject PlayerCastle;
    public GameObject EnemyCastle;

    private Player player;
    private Player enemy;
    private Player neutral;

    private List<Player> playerList = new List<Player>();
    //global timer
    private float interval = 1.0f;
    private float timer = 0.0f;

    // Resource income
    private bool goldIncome = false;
	// Use this for initialization
	void Start () {
        //Timer
        timer = Time.time + interval;
        
        //initialize unit types
        // string name, int atk, float health, int range, int movespeed, bool isRanged, int price, bool isStructure
        //,float armour, float armourPen, float attackSpeed
        UnitType soldierType = new UnitType("Soldier", 10, 50f, 0, 15, false,100, false, 0, 0, 1);
        UnitType skeletonType = new UnitType("Skeleton", 10, 50f, 0, 15, false, 100, false, 0, 0, 1);
        UnitType rangerType = new UnitType("Ranger", 7, 25f, 100, 12, true, 175,false, 0, 0f, 1);
        UnitType skeletonArcherType = new UnitType("Skeleton Archer", 7, 25f, 100, 12, true, 175, false, 0, 0f, 1);
        UnitType armouredSoldieType = new UnitType("Armoured Soldier", 3, 20f, 1, 10, false, 250, false, 0, 0, 1);
        UnitType armouredSkeletonType = new UnitType("Armoured Skeleton", 3, 20f, 1, 10, false, 250, false, 0, 0, 1);

        //initialize structure types
        UnitType towerSimple = new UnitType("Tower", 20, 100f, 150, 0, true, 500, true, 15f,10f,1.2f);
        
        //Initialize players
        //Ai
        enemy = new Player();
        enemy.playerColor = Color.red;
        enemy.Name = "enemy";
        enemy.unitTypeList.Add(soldierType);
        enemy.unitTypeList.Add(rangerType);
        enemy.unitTypeList.Add(armouredSoldieType);
        enemy.Income = 50;
        enemy.Resources = 500;

        playerList.Add(enemy);

        //Player
        player = new Player();
        player.playerColor = Color.cyan;
        player.Name = "player";
        player.unitTypeList.Add(skeletonType);
        player.unitTypeList.Add(skeletonArcherType);
        player.unitTypeList.Add(armouredSkeletonType);

        player.Income = 50;
        player.Resources = 500;

        playerList.Add(player);

        //Neutral
        // should be used for initial control
        neutral = new Player();
        neutral.playerColor = Color.gray;
        neutral.Name = "neutral";

        playerList.Add(neutral);
        
        //Setup basic spawn
        PlayerCastle.GetComponent<SpawnPoint>().Owner = player;
        PlayerCastle.GetComponent<Castle>().Owner = player;
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, player.unitTypeList[0], 10, player));
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, player.unitTypeList[1], 5, player));
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, player.unitTypeList[0], 10, player));
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, player.unitTypeList[1], 5, player));

        EnemyCastle.GetComponent<SpawnPoint>().Owner = enemy;
        EnemyCastle.GetComponent<Castle>().Owner = enemy;
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, enemy.unitTypeList[0], 10, enemy));
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, enemy.unitTypeList[1], 5, enemy));
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, enemy.unitTypeList[0], 10, enemy));
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, enemy.unitTypeList[1], 5, enemy));

        //initialize towns:
        List<GameObject> nodes = new List<GameObject>();
        nodes.Add(GameObject.Find("Town1"));
        nodes[0].GetComponent<CaptureNode>().setpropertyChange("Movespeed", 5);
        nodes.Add(GameObject.Find("Town2"));
        nodes[1].GetComponent<CaptureNode>().setpropertyChange("Attack", 1);
        nodes.Add(GameObject.Find("Town3"));
        nodes[2].GetComponent<CaptureNode>().setpropertyChange("Health", 4);
        nodes.Add(GameObject.Find("Town4"));
        nodes[3].GetComponent<CaptureNode>().setpropertyChange("Movespeed", 5);
        nodes.Add(GameObject.Find("Town5"));
        nodes[4].GetComponent<CaptureNode>().setpropertyChange("Health", 4);

        //initialize towers:
        /*
        GameObject tower = GameObject.Find("TowerSimple");
        tower.GetComponent<Unit>().Owner = neutral;
        tower.GetComponent<Unit>().Unittype = towerSimple;
	    */
	}

    void Update()
    {
        // Update player resource display
        //TODO should have a generic player lookup
        GameObject.Find("PlayerResources").guiText.text = "Gold: " + playerList[1].Resources;
        //Global timer update
        if (Time.time >= timer)
        {
            timer += interval;
            goldIncome = true;
        }
        //update gold

        if ((int)timer % 15 == 0 && goldIncome)
        {
            foreach (Player play in playerList)
            {
                play.Resources += play.Income;
            }
            goldIncome = false;
        }
        //Gets the postion of the mouse on camera
        Vector3 camCoord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(camCoord.x,camCoord.y);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos,new Vector2(0.0f,0.0f));
            if (hit)
            {
                //Debug.Log("Hit " + hit.transform.gameObject.name);
                // TODO: Add external check if menu is open, and close it!
                if (hit.transform.gameObject.tag == "Selectable")
                {
                    GameObject menu = Instantiate(castleMenu) as GameObject;
                    menu.GetComponent<MenuScript>().SetSpawnPoint(hit.transform.gameObject.GetComponent<SpawnPoint>());
                }
                else if (hit.transform.gameObject.tag == "CaptureNode" && hit.transform.gameObject.GetComponent<CaptureNode>().Owner == playerList[1]) //TODO fix for selecting player
                {
                    GameObject menu = Instantiate(townMenu) as GameObject;
                    menu.GetComponent<MenuPath>().setCaptureNode(hit.transform.gameObject.GetComponent<CaptureNode>());
                }
                else
                {
                    Debug.Log("Non selectable");
                }
            }
        }
    }

    /// <summary>
    /// gets the global timer
    /// </summary>
    /// <returns>time in floating value</returns>
    public float GetTimer()
    {
        return timer;
    }
}
