using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{

    private Dictionary<GameObject, int> spawnList;
    public Vector2 direction;

    private Player owner;

    // Use this for initialization
    void Start()
    {
        spawnList = new Dictionary<GameObject, int>();
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (KeyValuePair<GameObject, int> kvp in spawnList)
        {
            // starts a sub thread for spawning units to ensure delay
            StartCoroutine(SpawnFunction(kvp.Key, kvp.Value, owner));
            list.Add(kvp.Key);
        }
        foreach (GameObject obj in list)
        {
            spawnList.Remove(obj);
        }
    }

    IEnumerator SpawnFunction(GameObject obj, int amount, Player owner)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject troop = Instantiate(obj, new Vector3(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
            Unit troopUnit = troop.GetComponent<Unit>();
            troopUnit.setdirection(direction,false);
            troopUnit.Owner = owner;
            troop.gameObject.GetComponent<SpriteRenderer>().color = owner.playerColor;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void addUnits(GameObject type, int Amount, Player owner)
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
