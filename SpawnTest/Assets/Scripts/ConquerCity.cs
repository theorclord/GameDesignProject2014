using UnityEngine;
using System.Collections;

public class ConquerCity : MonoBehaviour {


    private int soldierCount;
	// Use this for initialization
	void Start () {
        soldierCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (soldierCount >= 10)
        {
            //Debug.Log("City conquered");
        }
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("Collision Trigger");
        soldierCount++;

    }
}
