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
    private bool owned;
	// Use this for initialization
	void Start () {
        owned = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log(coll.gameObject.GetComponent<Unit>().Owner.Name);
        if (coll.gameObject.GetComponent<Unit>() != null)
        {
            try
            {
                string name = coll.gameObject.GetComponent<Unit>().Owner.Name;
                if (name != this.Owner.Name)
                {
                    Destroy(coll.gameObject);
                    Debug.Log(name + " has won");
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(coll.transform.position);
                Debug.LogError("Object name" + coll.gameObject.name);
                
            }
        }
        
    }
}
