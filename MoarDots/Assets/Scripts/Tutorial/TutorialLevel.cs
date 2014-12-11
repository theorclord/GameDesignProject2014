using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<GameObject> nodes = new List<GameObject>();
        nodes.Add(GameObject.Find("gold_mine"));
        nodes[0].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
        nodes[0].GetComponent<CaptureNode>().setpropertyChange(true, "Income", "50");
        nodes.Add(GameObject.Find("forest"));
        nodes[1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Forest", typeof(Sprite)) as Sprite;
        nodes[1].GetComponent<CaptureNode>().setpropertyChange(false, "Health", "5");
        nodes[1].GetComponent<CaptureNode>().setpropertyChange(true, "Tech", "Forest");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
