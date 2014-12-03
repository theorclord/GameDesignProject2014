using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{

    private Dictionary<UnitType, int> spawnList;
    //public Vector2 direction;

    public GameObject TargetDirection;

    private Player owner;

    // Use this for initialization
    void Start()
    {
        spawnList = new Dictionary<UnitType, int>();
    }

    // Update is called once per frame
    void Update()
    {
        List<UnitType> list = new List<UnitType>();
        foreach (KeyValuePair<UnitType, int> kvp in spawnList)
        {
            // starts a sub thread for spawning units to ensure delay
            StartCoroutine(SpawnFunction(kvp.Key, kvp.Value, owner));
            list.Add(kvp.Key);
        }
        foreach (UnitType type in list)
        {
            spawnList.Remove(type);
        }
    }

    IEnumerator SpawnFunction(UnitType type, int amount, Player owner)
    {
		GameObject sound = GameObject.Find ("SoundManager");
		sound.GetComponent<SoundScript> ().playSound (99);
        for (int i = 0; i < amount; i++)
        {
            GameObject troop = Instantiate(Resources.Load("Prefab/Unit", typeof(GameObject)) as GameObject,
                    new Vector3(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
            Unit troopUnit = troop.GetComponent<Unit>();            
            troopUnit.setUnitType(type);
            troopUnit.setdirection(TargetDirection.transform.position, false);
            troopUnit.Owner = owner;
            troop.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/" + troopUnit.Name, typeof(Sprite)) as Sprite;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void addUnits(UnitType type, int Amount, Player owner)
    {
        this.owner = owner;
        if (spawnList.ContainsKey(type))
        {
            spawnList[type] = spawnList[type] + Amount;
        }
        else
        {
            spawnList.Add(type, Amount);
        }
    }
}
