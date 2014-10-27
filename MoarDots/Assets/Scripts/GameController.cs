using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject castleMenu;

    public GameObject Soldier;

    public GameObject PlayerCastle;
    public GameObject EnemyCastle;

    private Player player;
    private Player enemy;

	// Use this for initialization
	void Start () {

		UnitType soldierType = new UnitType ();
		UnitType skeletonType = new UnitType ();
		
		soldierType.setValues ("soldier",1,1,1,10);
		skeletonType.setValues ("skeleton",1,1,1,10);

        enemy = new Player();
        enemy.playerColor = Color.red;
        enemy.Name = "enemy";
        enemy.unitList.Add(
            Instantiate(Resources.Load("Prefab/Unit", typeof(GameObject)) as GameObject,
            new Vector3(-500.0f,-500.0f,100.0f),Quaternion.identity) as GameObject);
		// Add the UnitType to the UnitList items


		enemy.unitList[0].gameObject.GetComponent<Unit>().setUnitType(soldierType);
        //enemy.unitList[0].gameObject.GetComponent<Unit>().Movespeed = 10;
        enemy.unitList[0].gameObject.GetComponent<Unit>().Owner = enemy;


        player = new Player();
        player.playerColor = Color.cyan;
        player.Name = "player";
		player.unitList.Add(
			Instantiate(Resources.Load("Prefab/Unit", typeof(GameObject)) as GameObject,
		            new Vector3(500.0f,-500.0f,100.0f),Quaternion.identity) as GameObject);
		//player.unitList.Add(Instantiate(Resources.Load("Prefab/Skeleton",typeof(GameObject)) as GameObject) as GameObject);
		player.unitList[0].gameObject.GetComponent<Unit>().setUnitType(skeletonType);
		//player.unitList[0].gameObject.GetComponent<Unit>().Movespeed = 10;
		player.unitList[0].gameObject.GetComponent<Unit>().Owner = player;

        PlayerCastle.GetComponent<SpawnPoint>().Owner = player;
        PlayerCastle.GetComponent<Castle>().Owner = player;
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, player.unitList[0], 10, player));
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, player.unitList[0], 10, player));

        EnemyCastle.GetComponent<SpawnPoint>().Owner = enemy;
        EnemyCastle.GetComponent<Castle>().Owner = enemy;
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, enemy.unitList[0], 10, enemy));
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, enemy.unitList[0], 10, enemy));

        //initialize towns:
        List<GameObject> nodes = new List<GameObject>();
        nodes.Add(GameObject.Find("Town1"));
        nodes[0].GetComponent<CaptureNode>().setpropertyChange("Movespeed", 0);
        nodes.Add(GameObject.Find("Town2"));
        nodes[1].GetComponent<CaptureNode>().setpropertyChange("Movespeed", 0);
        nodes.Add(GameObject.Find("Town3"));
        nodes[2].GetComponent<CaptureNode>().setpropertyChange("Movespeed", 0);
        nodes.Add(GameObject.Find("Town4"));
        nodes[3].GetComponent<CaptureNode>().setpropertyChange("Movespeed", 0);
        nodes.Add(GameObject.Find("Town5"));
        nodes[4].GetComponent<CaptureNode>().setpropertyChange("Movespeed", 0);
	
	}

    void Update()
    {
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
                else
                {
                    Debug.Log("Non selectable");
                }
            }
        }
    }
}
