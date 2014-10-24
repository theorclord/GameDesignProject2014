using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class City : MonoBehaviour {
    public Vector2 Direction;

    public List<Vector2> DirectionPlayer;
    public List<Vector2> DirectionEnemy;
    int nextPathEnemy;
    int nextPathPlayer;


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
        soldierCount++;

        Unit troop = coll.GetComponent<Unit>();
        if (troop != null)
        {
            if (troop != null && troop.Owner.Name == "player")
            {
                troop.setdirection(DirectionPlayer[nextPathPlayer]);
                nextPathPlayer++;
                if (nextPathPlayer >= DirectionPlayer.Count)
                {
                    nextPathPlayer = 0;
                }
            }
            else
            {
                troop.setdirection(DirectionEnemy[nextPathEnemy]);
                nextPathEnemy++;
                if (nextPathEnemy >= DirectionEnemy.Count)
                {
                    nextPathEnemy = 0;
                }
            }
        }
        //coll.GetComponent<Unit>().setdirection(Direction);
    }
}
