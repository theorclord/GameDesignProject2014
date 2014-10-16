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
        //StartCoroutine(SpawnFunction());
        List<GameObject> list = new List<GameObject>();
        foreach (KeyValuePair<GameObject, int> kvp in spawnList)
        {
            StartCoroutine(SpawnFunction(kvp.Key, kvp.Value));
            list.Add(kvp.Key);
        }
        foreach (GameObject obj in list)
        {
            spawnList.Remove(obj);
        }
    }

    IEnumerator SpawnFunction(GameObject obj, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject troop = Instantiate(obj, new Vector3(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
            troop.GetComponent<Unit>().setdirection(direction);
            yield return new WaitForSeconds(0.5f);
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
