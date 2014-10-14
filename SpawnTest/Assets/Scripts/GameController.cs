using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject MouseCursor;
    private GameObject mouse;

	// Use this for initialization
	void Start () {
        //mouse = Instantiate(MouseCursor, Input.mousePosition, Quaternion.identity) as GameObject;
	
	}

    void Update()
    {
        Vector3 camCoord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(camCoord.x,camCoord.y);
        //mouse.transform.position = new Vector3(camCoord.x, camCoord.y);


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos,new Vector2(0.0f,0.0f));
            if (hit)
            {
                Debug.Log("Hit " + hit.transform.gameObject.name);
                if (hit.transform.gameObject.tag == "Selectable")
                {
                    List<GameObject> spawners = hit.transform.gameObject.GetComponent<SpawnPoint>().getSpawners();
                    List<string> spawnNames = new List<string>();
                    foreach (GameObject spwn in spawners)
                    {
                        spawnNames.Add(spwn.gameObject.name);
                    }
                    Debug.Log("Object selected");
                }
                else
                {
                    Debug.Log("Non selectable");
                }
            }
            else
            {
                Debug.Log("No hit");
            }
        }
    }
}
