using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{

    private Dictionary<UnitType, int> spawnList;
    public Vector2 direction;

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
        for (int i = 0; i < amount; i++)
        {
            GameObject troop = Instantiate(Resources.Load("Prefab/Unit", typeof(GameObject)) as GameObject,
                    new Vector3(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
            Unit troopUnit = troop.GetComponent<Unit>();
            troopUnit.setUnitType(type);
            troopUnit.setdirection(direction,false);
            troopUnit.Owner = owner;
            troop.gameObject.GetComponent<SpriteRenderer>().color = owner.playerColor;
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

    public void setDirection(Vector2 vec)
    {
        direction = vec;
    }
}
