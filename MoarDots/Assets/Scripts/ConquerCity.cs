using UnityEngine;
using System.Collections;

public class ConquerCity : MonoBehaviour {


    private int soldierCount;
    private bool owned;
	// Use this for initialization
	void Start () {
        soldierCount = 0;
        owned = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (soldierCount >= 1 && !owned)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("PlayerCastleCircle", typeof(Sprite)) as Sprite;
            owned = true;
            //Debug.Log("City conquered");
        }
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("Collision Trigger");
        soldierCount++;
        GameObject obj = GameObject.Find("SpawnTown1North");
        GameObject troop = Resources.Load("Prefab/"+coll.gameObject.name.Remove(coll.gameObject.name.Length - 7),typeof(GameObject)) as GameObject;

        obj.GetComponent<UnitSpawner>().addUnits(troop, 1, coll.gameObject.GetComponent<Unit>().Owner);
        Destroy(coll.gameObject);
    }
}
