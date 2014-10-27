using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaptureNode : MonoBehaviour {

    public List<Vector2> DirectionPlayer;
    public List<Vector2> DirectionEnemy;
    int nextPathEnemy;
    int nextPathPlayer;


    public List<GameObject> spawners;
    private List<SpawnPair> state;

    private Dictionary<Player, List<GameObject>> ownedSoldierCount = new Dictionary<Player, List<GameObject>>();
    private bool contested = false;
    private bool owned;
    private Player owner;
    // Use this for initialization
    void Start()
    {
        owned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (contested)
        {
            int count = ownedSoldierCount.Count;
            if (count > 0)
            {
                int max = 0;
                Player tempOwner = null;
                foreach (KeyValuePair<Player, List<GameObject>> kvp in ownedSoldierCount)
                {
                    if (kvp.Value.Count > max)
                    {
                        List<int> indexes = new List<int>();
                        for (int i = 0; i < kvp.Value.Count; i++)
                        {
                            if (kvp.Value[i] == null)
                            {
                                indexes.Add(i);
                            }
                        }
                        for (int i = indexes.Count-1; i >= 0; i--)
                        {
                            kvp.Value.RemoveAt(indexes[i]);
                        }

                        max = kvp.Value.Count;
                        tempOwner = kvp.Key;
                    }
                }
                if (tempOwner != null)
                {
                    owner = tempOwner;
                }
            }
            transform.gameObject.GetComponent<SpriteRenderer>().color = owner.playerColor;
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        Unit troop = coll.GetComponent<Unit>();
        if (troop != null)
        {
            
            Player player = troop.Owner;
            if (!contested && (owner == null || owner.Name != player.Name))
            {
                contested = true;
            }
            if (ownedSoldierCount.ContainsKey(player))
            {
                ownedSoldierCount[player].Add(coll.gameObject);
            }
            else
            {
                ownedSoldierCount.Add(player, new List<GameObject>() { coll.gameObject });
            }
            // TODO player name should be generic
            if (troop != null && player.Name == "player")
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
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Unit troop = coll.GetComponent<Unit>();
        if (troop != null)
        {
            Player player = troop.Owner;
            ownedSoldierCount[player].Remove(coll.gameObject);
            if (ownedSoldierCount.Count <= 1)
            {
                contested = false;
            }
        }
    }
}
