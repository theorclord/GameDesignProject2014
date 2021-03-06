﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;

public class CaptureNode : MonoBehaviour {

    public string Technologi;

    //target locations
    public List<GameObject> TargetPlayer;
    public List<GameObject> TargetAI;
    int nextPathEnemy;
    int nextPathPlayer;
    public List<int> CustomDirection;
	public List<Texture> arrows;

    // attached spawners for unit spawning
    public List<GameObject> spawners;

    // capture variables
    private Dictionary<Player, List<GameObject>> ownedSoldierCount = new Dictionary<Player, List<GameObject>>();
    private bool contested = false;
    public Player Owner
    {
        get;
        private set;
    }

    // unit property change variables
    private Dictionary<bool, PropertyPair> propertyChange =
        new Dictionary<bool, PropertyPair>();

    // Use this for initialization
    void Start()
    {
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
                    // removes add on
                    if (Owner != null)
                    {
                        setProperty(Owner, -1.0f);
                    }
                    // adds add on
                    setProperty(tempOwner, 1.0f);
                    Owner = tempOwner;
                }
            }
            // changes color of the node
            //transform.gameObject.GetComponent<SpriteRenderer>().color = Owner.playerColor;
			if(Owner.playerColor == Color.cyan){
				if(Technologi == "mine"){
					transform.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/GoldMineEvil", typeof(Sprite)) as Sprite;
				}else if(Technologi == "forest"){
					transform.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/ForestEvil", typeof(Sprite)) as Sprite;
				}
			}else{
				if(Technologi == "mine"){
					transform.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/GoldMineFairy", typeof(Sprite)) as Sprite;
				}else if(Technologi == "forest"){
					transform.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/ForestFairy", typeof(Sprite)) as Sprite;
				}
			}
            contested = false;
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
            // TODO player name should be generic
            if (troop != null && player.Name == "player")
            {
                if (CustomDirection.Count > 0)
                {
                    if (CustomDirection.Count > 1)
                    {
                        if (nextPathPlayer >= CustomDirection.Count)
                        {
                            nextPathPlayer = 0;
                        }
                        troop.setdirection(TargetPlayer[CustomDirection[nextPathPlayer]].transform.position, false);
                        nextPathPlayer++;
                    }
                    else
                    {
                        troop.setdirection(TargetPlayer[CustomDirection[0]].transform.position, false);
                    }
                    /*
                    int? count = 100;
                    int random = (int)Random.Range(1,(int)count);
                    
                    for (int i = 0; i < CustomDirection.Count; i++)
                    {
                        if (random < CustomDirection[i])
                        {
                            troop.setdirection(TargetPlayer[i].transform.position, false);
                        }
                        else
                        {
                            count -= CustomDirection[i];
                            random = (int)Random.Range(1, (int)count);
                        }
                    }
                     */
                }
                else
                {
                    troop.setdirection(TargetPlayer[nextPathPlayer].transform.position,false);
                    nextPathPlayer++;
                    if (nextPathPlayer >= TargetPlayer.Count)
                    {
                        nextPathPlayer = 0;
                    }
                }
            }
            else
            {
                troop.setdirection(TargetAI[nextPathEnemy].transform.position,false);
                nextPathEnemy++;
                if (nextPathEnemy >= TargetAI.Count)
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
        foreach (KeyValuePair<bool, PropertyPair> kvp in propertyChange)
        {
            //if true, player, if false units
            if (kvp.Key)
            {
                owner.setProperty(kvp.Value, mod);
                if (mod > 0)
                {
                    owner.Technology.Add(kvp.Value.Identifier);
                }
                else
                {
                    owner.Technology.Remove(kvp.Value.Identifier);
                }
            }
            else
            {
                foreach (UnitType type in owner.unitTypeList)
                {
                    type.updateUnitType(kvp.Value.Identifier, mod *float.Parse( kvp.Value.Val));
                }
            }
        }
        
    }

    public void setpropertyChange(bool actor,string prop, string val)
    {
        // should never have to change after initialization
        propertyChange.Add(actor,new PropertyPair(prop,val));
    }
}
