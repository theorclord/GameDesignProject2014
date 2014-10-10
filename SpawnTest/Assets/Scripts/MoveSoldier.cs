using UnityEngine;
using System.Collections;

public class MoveSoldier : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.rigidbody2D.velocity = new Vector2(1.0f, 0.0f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
