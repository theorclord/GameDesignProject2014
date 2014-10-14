using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {

    public GameObject Soldier;
    //public GameObject spawner;
    public List<GameObject> spawners;
    // int is the path number, gameobject is the unit spawned.
    private List<SpawnPair> state;
    
    private int numberOfPaths;
    private float interval = 1.0f;
    private float timer = 0.0f;

    private bool spawn;
    // Use this for initialization
    void Start()
    {
        state = new List<SpawnPair>();
        numberOfPaths = spawners.Count;
        timer = Time.time + interval;
        spawn = false;

        //test code;
        state.Add(new SpawnPair(0, Soldier));
        state.Add(new SpawnPair(1, Soldier));
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
            foreach (SpawnPair pair in state)
            {
                spawners[pair.Path].GetComponent<UnitSpawner>().addUnits(pair.UnitType);
            }
            spawn = false;
        }
    }

    public List<GameObject> getSpawners()
    {
        return spawners;
    }

    public void addState(GameObject unit, int path)
    {
        state.Add(new SpawnPair( path, unit ));
    }
}
