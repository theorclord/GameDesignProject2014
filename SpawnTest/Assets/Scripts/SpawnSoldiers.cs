using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnSoldiers : MonoBehaviour {
    public GameObject Soldier;
    public GameObject spawner;
    public List<GameObject> spawners;


    private float interval = 1.0f;
    private float timer = 0.0f;

    private bool spawn;
	// Use this for initialization
	void Start () {
        timer = Time.time + interval;
        spawn = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= timer)
        {
            timer += interval;
            spawn = true;
        }
        int spawnCount = (int) timer;
        //Debug.Log(spawnCount);
        if (spawnCount % 5 == 0 && spawn)
        {
            foreach (GameObject spw in spawners)
            {
                spw.GetComponent<UnitSpawner>().addUnits(Soldier, 10);
            }
            spawn = false;
        }
	}
}
