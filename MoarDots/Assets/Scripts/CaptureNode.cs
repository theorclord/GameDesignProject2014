using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaptureNode : MonoBehaviour {

    // Path selection
    public List<Vector2> DirectionPlayer;
    public List<Vector2> DirectionEnemy;
    int nextPathEnemy;
    int nextPathPlayer;
    public List<int?> customDirection;
    private int customCounter = 0;

    // attached spawners for unit spawning
    public List<GameObject> spawners;

    // capture variables
    private Dictionary<Player, List<GameObject>> ownedSoldierCount = new Dictionary<Player, List<GameObject>>();
    private bool contested = false;
    private bool owned;
    private Player owner;

    // unit property change variables
    private Dictionary<string, float> propertyChange = new Dictionary<string,float>();

    // Use this for initialization
    void Start()
    {
        owned = false;
    }

    // Update is called once per frame
    void Update()
    {
        // only updates if the city is contested
        if (contested)
        {
            // counts number of contesters
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
                // check if owner is null, to prevent change if no units are nearby
                if (tempOwner != null)
                {
                    //TODO if more players are present, need to update
                    // removes add on
                    if (owner != null)
                    {
                        setProperty(owner, -1.0f);
                    }
                    // adds add on
                    setProperty(tempOwner, 1.0f);
                    owner = tempOwner;
                }
            }
            // changes color of the node
            transform.gameObject.GetComponent<SpriteRenderer>().color = owner.playerColor;
        }
    }
    #region triggers
    void OnTriggerEnter2D(Collider2D coll)
    {
        
        Unit troop = coll.GetComponent<Unit>();
        // checks if collider is non unit
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
                if (customDirection != null)
                {
                    int? count = 100;
                    int random = (int)Random.Range(1,(int)count);
                    
                    for (int i = 0; i < customDirection.Count; i++)
                    {
                        if (random < customDirection[i])
                        {
                            troop.setdirection(DirectionPlayer[i],false);
                        }
                        else
                        {
                            count -= customDirection[i];
                            random = (int)Random.Range(1, (int)count);
                        }
                    }
                }
                else
                {
                    troop.setdirection(DirectionPlayer[nextPathPlayer],false);
                    nextPathPlayer++;
                    if (nextPathPlayer >= DirectionPlayer.Count)
                    {
                        nextPathPlayer = 0;
                    }
                }
            }
            else
            {
                troop.setdirection(DirectionEnemy[nextPathEnemy],false);
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
    #endregion

    private void setProperty(Player owner, float mod)
    {
        foreach (UnitType type in owner.unitTypeList)
        {
            foreach (KeyValuePair<string, float> kvp in propertyChange)
            {
                type.updateUnitType(kvp.Key, mod*kvp.Value);
            }
        }
    }

    public void setpropertyChange(string prop, float val)
    {
        // should never have to change after initialization
        propertyChange.Add(prop, val);
    }
}
