using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    public Player Owner
    {
        set;
        get;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if(coll.gameObject.GetComponent<Unit>() !=null){
            Debug.Log(coll.GetComponent<Unit>());
        }
    }
}
