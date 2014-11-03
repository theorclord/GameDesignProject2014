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

    private float lastSpawn;
    private bool spawn;

    // Use this for initialization
    void Start()
    {
        spawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        float timer = GameObject.Find("GameController").GetComponent<GameController>().GetTimer();
        if (lastSpawn < timer)
        {
            lastSpawn = timer;
            spawn = true;
        }
        int spawnCount = (int)timer;
        if ((spawnCount % 15 == 0 || spawnCount < 2) && spawn)
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
