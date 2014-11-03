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
        UnitType soldierType = new UnitType("Soldier", 1, 10, 1, 10, false, 100);
        UnitType skeletonType = new UnitType("Skeleton", 1, 10, 1, 10, false, 100);
        UnitType rangerType = new UnitType("Ranger", 1,10,100,10,true, 175);
        UnitType skeletonArcherType = new UnitType("Skeleton Archer", 1, 10, 100, 10, true,175);

        //Initialize players
        enemy = new Player();
        enemy.playerColor = Color.red;
        enemy.Name = "enemy";
        enemy.unitTypeList.Add(soldierType);
        enemy.unitTypeList.Add(rangerType);
        enemy.Income = 50;
        enemy.Resources = 500;

        playerList.Add(enemy);

        player = new Player();
        player.playerColor = Color.cyan;
        player.Name = "player";
        player.unitTypeList.Add(skeletonType);
        player.unitTypeList.Add(skeletonArcherType);
        player.Income = 50;
        player.Resources = 500;

        playerList.Add(player);

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
                Debug.Log("Hit " + hit.transform.gameObject.name);
                if (hit.transform.gameObject.tag == "Selectable")
                {
                    GameObject menu = Instantiate(castleMenu) as GameObject;
                    menu.GetComponent<MenuScript>().setSpawnPoint(hit.transform.gameObject.GetComponent<SpawnPoint>());
                }
                else if (hit.transform.gameObject.tag == "CaptureNode")
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


    public float GetTimer()
    {
        return timer;
    }
}
