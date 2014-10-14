using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {

    public GameObject Soldier;
    //public GameObject spawner;
    public List<GameObject> spawners;
    // int is the path number, gameobject is the unit spawned.
    private List<object[]> state;
    
    private int numberOfPaths;
    private float interval = 1.0f;
    private float timer = 0.0f;

    private bool spawn;
    // Use this for initialization
    void Start()
    {
        state = new List<object[]>();
        numberOfPaths = spawners.Count;
        timer = Time.time + interval;
        spawn = false;

        //test code;
        state.Add(new object[]{0, Soldier});
        state.Add(new object[] { 1, Soldier });
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timer)
        {
            timer += interval;
            spawn = true;
        }
        int spawnCount = (int)timer;
        //Debug.Log(spawnCount);
        if (spawnCount % 5 == 0 && spawn)
        {
            foreach (object[] objList in state)
            {
                int path = (int) objList[0];
                GameObject unit = (GameObject) objList[1];
                spawners[path].GetComponent<UnitSpawner>().addUnits(unit);
            }
            spawn = false;
        }
    }

    public List<GameObject> getSpawners()
    {
        return spawners;
    }

    public void setState()
    {

    }
}
