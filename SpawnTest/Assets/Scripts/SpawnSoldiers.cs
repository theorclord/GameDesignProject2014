using UnityEngine;
using System.Collections;

public class SpawnSoldiers : MonoBehaviour {
    public GameObject Soldier;

    private int count;

    private float interval = 1.0f;
    private float timer = 0.0f;
	// Use this for initialization
	void Start () {
        count = 0;
        timer = Time.time + interval;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= timer)
        {
            timer += interval;

        }
        int spawnCount = (int) timer;
        Debug.Log(spawnCount);
        if (spawnCount % 30 == 0)
        {
            count = 0;
        }
        if (count < 1)
        {
            Instantiate(Soldier, new Vector3(1.3f, 0.0f), Quaternion.identity);
            count++;
        }
	}
}
