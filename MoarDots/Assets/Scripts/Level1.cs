using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Level1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<GameObject> nodes = new List<GameObject>();
        nodes.Add(GameObject.Find("Town1"));
        nodes[0].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
        nodes[0].GetComponent<CaptureNode>().setpropertyChange(true, "Income", "10");
        nodes.Add(GameObject.Find("Town2"));
        nodes[1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Forest", typeof(Sprite)) as Sprite;
        nodes[1].GetComponent<CaptureNode>().setpropertyChange(false, "Health", "5");
        nodes[1].GetComponent<CaptureNode>().setpropertyChange(true, "Tech", "Forest");
        nodes.Add(GameObject.Find("Town3"));
        nodes[2].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
        nodes[2].GetComponent<CaptureNode>().setpropertyChange(true, "Income", "10");
        nodes.Add(GameObject.Find("Town4"));
        nodes[3].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
        nodes[3].GetComponent<CaptureNode>().setpropertyChange(true, "Income", "10");
        nodes.Add(GameObject.Find("Town5"));
        nodes[4].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
        nodes[4].GetComponent<CaptureNode>().setpropertyChange(true, "Income", "10");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
