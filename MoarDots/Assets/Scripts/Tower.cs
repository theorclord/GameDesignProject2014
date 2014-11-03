using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

    private UnitType towerType;


    //Ownership
    public bool IsRuin
    {
        get;
        set;
    }
    public Player Owner
    {
        set;
        get;
    }
    private bool contested;
    private Dictionary<Player, List<GameObject>> ownedSoldierCount = new Dictionary<Player, List<GameObject>>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // only updates if the tower is contested
        if (contested && IsRuin)
        {
            // counts number of contesters
            int count = ownedSoldierCount.Count;
            if (count > 0)
            {
                // remove any null values:
                foreach (KeyValuePair<Player, List<GameObject>> kvp in ownedSoldierCount)
                {
                    List<int> indexes = new List<int>();
                    for (int i = 0; i < kvp.Value.Count; i++)
                    {
                        if (kvp.Value[i] == null)
                        {
                            indexes.Add(i);
                        }
                    }
                    for (int i = indexes.Count - 1; i >= 0; i--)
                    {
                        kvp.Value.RemoveAt(indexes[i]);
                    }
                }
                //check which owner has the most units
                int max = 0;
                Player tempOwner = null;
                foreach (KeyValuePair<Player, List<GameObject>> kvp in ownedSoldierCount)
                {
                    if (kvp.Value.Count > max)
                    {
                        max = kvp.Value.Count;
                        tempOwner = kvp.Key;
                    }
                }
                // check if owner is null, to prevent change if no units are nearby
                if (tempOwner != null)
                {
                    Owner = tempOwner;
                }
            }
            // changes color of the node
            transform.gameObject.GetComponent<SpriteRenderer>().color = Owner.playerColor;
            contested = false;
        }
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Unit target = coll.gameObject.GetComponentInParent<Unit>();
        if (target != null)
        {
            Player player = target.Owner;
            if (!contested && (Owner == null || Owner.Name != player.Name))
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
        }
    }
}
