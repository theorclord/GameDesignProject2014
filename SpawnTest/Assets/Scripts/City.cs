using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class City : MonoBehaviour {
    public Vector2 Direction;

    public List<GameObject> spawners;
    private List<SpawnPair> state;

    private int soldierCount;
    private bool owned;
    // Use this for initialization
    void Start()
    {
        soldierCount = 0;
        owned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (soldierCount >= 1 && !owned)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("PlayerCastleCircle", typeof(Sprite)) as Sprite;
            owned = true;
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("Collision Trigger");
        soldierCount++;
        coll.GetComponent<Unit>().setdirection(Direction);
        /*
        GameObject obj = GameObject.Find("SpawnTown1North");
        GameObject troop = Resources.Load("Prefab/" + coll.gameObject.name.Remove(coll.gameObject.name.Length - 7), typeof(GameObject)) as GameObject;
        obj.GetComponent<UnitSpawner>().addUnits(troop, 1);
        Destroy(coll.gameObject);
         */
    }
}
