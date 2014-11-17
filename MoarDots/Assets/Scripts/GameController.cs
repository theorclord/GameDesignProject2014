using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    private float GoldTimer = 5;
    public float SpawnTimer = 15;
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
    private float startTime;

    // Resource income checker
    private bool goldIncome = false;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
        //Timer
        timer = Time.time + interval-startTime;
        
        //initialize unit types
        // string name, int atk, float health, int range, int movespeed, bool isRanged, int price, bool isStructure
        //,float armour, float armourPen, float attackSpeed, unit tier
        UnitType soldierType = new UnitType("Soldier", 10f, 50f, 100, 15, false,100, false, 0, 0, 1, UnitType.LOWER_TIER, "None");
        UnitType skeletonType = new UnitType("Zombie", 10, 50f, 100, 15, false, 100, false, 0, 0, 1, UnitType.LOWER_TIER, "None");
        UnitType fairyType = new UnitType("Fairy", 7f, 25f, 300, 12, true, 175, false, 0, 0f, 1, UnitType.LOWER_TIER, "None");
        UnitType skeletonArcherType = new UnitType("Skeleton Archer", 7f, 25f, 300, 12, true, 175, false, 0, 0f, 1, UnitType.LOWER_TIER, "None");
        UnitType evilMooseType = new UnitType("Evil Moose", 15f, 400, 100, 10, false, 800, false, 5, 0, 1, UnitType.HIGHER_TIER, "Forest");
        UnitType centaurType = new UnitType("Centaur", 15f, 400, 100, 10, false, 800, false, 5, 0, 1, UnitType.HIGHER_TIER, "Forest");
        
        //initialize structure types
        //TODO
        UnitType towerSimple = new UnitType("Tower", 20, 500f, 200, 0, true, 500, true, 15f, 10f, 1f, UnitType.LOWER_TIER, "None");
        
        //Initialize players
        //Ai
        enemy = new Player();
        enemy.playerColor = Color.red;
        enemy.Name = "enemy";
        enemy.unitTypeList.Add(soldierType);
        enemy.unitTypeList.Add(fairyType);
        enemy.unitTypeList.Add(centaurType);
        enemy.Income = 50;
        enemy.Resources = 500;
        enemy.Technology.Add("None");
        GameObject.Find("AIController").GetComponent<AI>().SetAI(enemy);
        playerList.Add(enemy);

        //Player
        player = new Player();
        player.playerColor = Color.cyan;
        player.Name = "player";
        player.unitTypeList.Add(skeletonType);
        player.unitTypeList.Add(skeletonArcherType);
        player.unitTypeList.Add(evilMooseType);

        player.Income = 50;
        player.Resources = 1000;
        player.Technology.Add("None");

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
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, player.unitTypeList[0], 10, player));

        EnemyCastle.GetComponent<SpawnPoint>().Owner = enemy;
        EnemyCastle.GetComponent<Castle>().Owner = enemy;
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, enemy.unitTypeList[0], 10, enemy));
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, enemy.unitTypeList[0], 10, enemy));

        //initialize towns:
        List<GameObject> nodes = new List<GameObject>();
        nodes.Add(GameObject.Find("Town1"));
        nodes[0].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
        nodes[0].GetComponent<CaptureNode>().setpropertyChange(true, "Income", "50");
        nodes.Add(GameObject.Find("Town2"));
        nodes[1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Forest", typeof(Sprite)) as Sprite;
        nodes[1].GetComponent<CaptureNode>().setpropertyChange(false, "Health", "5");
        nodes[1].GetComponent<CaptureNode>().setpropertyChange(true, "Tech", "Forest");
        nodes.Add(GameObject.Find("Town3"));
        nodes[2].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
        nodes[2].GetComponent<CaptureNode>().setpropertyChange(true, "Income", "50");
        nodes.Add(GameObject.Find("Town4"));
        nodes[3].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
        nodes[3].GetComponent<CaptureNode>().setpropertyChange(true, "Income", "50");
        nodes.Add(GameObject.Find("Town5"));
        nodes[4].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
        nodes[4].GetComponent<CaptureNode>().setpropertyChange(true, "Income", "50");

        //initialize towers:
        /*
        setTower("Tower1", towerSimple);
        setTower("Tower2", towerSimple);
        setTower("Tower3", towerSimple);
        setTower("Tower4", towerSimple);
        setTower("Tower5", towerSimple);
        setTower("Tower6", towerSimple);
	    */
	}

    private void setTower(string Name, UnitType type)
    {
        GameObject tower = GameObject.Find(Name);
        tower.GetComponent<Unit>().Owner = neutral;
        tower.GetComponent<Unit>().Unittype = type;
        tower.GetComponent<Building>().RuinSprite = "Sprites/TowerRuin";
        tower.GetComponent<Building>().BuildingSprite = "Sprites/Tower";
        tower.GetComponent<Building>().Name = "Tower";
        tower.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Tower", typeof(Sprite)) as Sprite;
    }

    void Update()
    {
        // Update player resource display
        //TODO should have a generic player lookup
        GameObject.Find("PlayerResources").guiText.text = "Gold: " + playerList[1].Resources;
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
            else if(Mathf.Floor(timer /60) >9)
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
                str = ""+timer % 60;
            }
            GameObject.Find("Timer").guiText.text = "Timer: " + "00:" + str;
            
        }
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
