using UnityEngine;
using System.Collections;

public class SpawnSoldiers : MonoBehaviour {
    public GameObject Soldier;
    public GameObject spawner;


    private float interval = 1.0f;
    private float timer = 0.0f;

    private bool spawn;
	// Use this for initialization
	void Start () {
        spawner.GetComponent<UnitSpawner>().setDirection(new Vector2(3.0f, 1.0f));
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
        Debug.Log(spawnCount);
        if (spawnCount % 5 == 0 && spawn)
        {
            spawner.GetComponent<UnitSpawner>().addUnits(Soldier, 10);
            spawn = false;
        }
	}
}
