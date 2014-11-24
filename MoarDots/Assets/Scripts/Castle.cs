using UnityEngine;
using System.Collections;
using System;

public class Castle : MonoBehaviour {

    // should be replaced captureNode in later editions
    public Player Owner
    {
        get;
        set;
    }
    private int soldierCount;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Unit>() != null)
        {
            string name = coll.gameObject.GetComponent<Unit>().Owner.Name;
            if (name != this.Owner.Name)
            {
                Destroy(coll.gameObject);
                //TODO make non hard coded
                if (name == "player")
                {
                    Application.LoadLevel("WinningScreen");
                }
                else
                {
                    Application.LoadLevel("LoosingScreen");
                }
            }
        }
        
    }
}
