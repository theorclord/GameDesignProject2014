using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {

    public List<GameObject> spawners;

    public Player Owner
    {
        get;
        set;
    }
    private List<SpawnPair> state = new List<SpawnPair>();
    
    private int numberOfPaths;
    private float interval = 1.0f;
    private float timer = 0.0f;

    private bool spawn;

    // Use this for initialization
    void Start()
    {
        numberOfPaths = spawners.Count;
        timer = Time.time + interval;
        spawn = false;
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
        if (spawnCount % 15 == 0 && spawn)
        {
            foreach (SpawnPair pair in state)
            {
                spawners[pair.Path].GetComponent<UnitSpawner>().addUnits(pair.UnitType, pair.Amount, Owner);
            }
            spawn = false;
        }
    }

    public List<GameObject> getSpawners()
    {
        return spawners;
    }


    public void addState(SpawnPair spawn)
    {
        state.Add(spawn);
    }

    public void clearStates()
    {
        state.Clear();
    }

    public List<SpawnPair> getStates()
    {
        return state;
    }
}
