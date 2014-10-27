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
        enemy = new Player();
        enemy.playerColor = Color.red;
        enemy.Name = "enemy";

        player = new Player();
        player.playerColor = Color.cyan;
        player.Name = "player";

        PlayerCastle.GetComponent<SpawnPoint>().Owner = player;
        PlayerCastle.GetComponent<Castle>().Owner = player;
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, Soldier, 10, player));
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, Soldier, 10, player));

        EnemyCastle.GetComponent<SpawnPoint>().Owner = enemy;
        EnemyCastle.GetComponent<Castle>().Owner = enemy;
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, Soldier, 10, enemy));
        EnemyCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, Soldier, 10, enemy));
	
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
