using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{

    private Dictionary<GameObject, int> spawnList;
    public Vector2 direction;

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
            GameObject troop = Instantiate(kvp.Key, new Vector3(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
            troop.GetComponent<Unit>().setdirection(direction);
            list.Add(kvp.Key);
        }
        foreach (GameObject obj in list)
        {
            spawnList.Remove(obj);
        }

    }

    public void addUnits(GameObject type, int Amount)
    {
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
