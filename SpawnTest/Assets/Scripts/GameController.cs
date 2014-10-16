using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject castleMenu;

    public GameObject Soldier;

    public GameObject PlayerCastle;

	// Use this for initialization
	void Start () {
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(0, Soldier, 10));
        PlayerCastle.GetComponent<SpawnPoint>().addState(new SpawnPair(1, Soldier, 10));
	
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
                    List<GameObject> spawners = hit.transform.gameObject.GetComponent<SpawnPoint>().getSpawners();
                    GameObject menu = Instantiate(castleMenu) as GameObject;
                    menu.GetComponent<MenuScript>().RecieveList(spawners);
                    Debug.Log("Object selected");
                }
                else
                {
                    Debug.Log("Non selectable");
                }
            }
        }
    }
}
