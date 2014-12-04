using UnityEngine;
using System.Collections;

public class Level2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        for (int i = 1; i < 9; i++)
        {
            GameObject.Find("Mine" + i).GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Goldmine", typeof(Sprite)) as Sprite;
            GameObject.Find("Mine" + i).GetComponent<CaptureNode>().setpropertyChange(true, "Income", "10");
        }
        for (int i = 1; i < 3; i++)
        {
            GameObject.Find("Forest" + i).GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Forest", typeof(Sprite)) as Sprite;
            GameObject.Find("Forest" + i).GetComponent<CaptureNode>().setpropertyChange(false, "Health", "10");
            GameObject.Find("Forest" + i).GetComponent<CaptureNode>().setpropertyChange(true, "Tech", "Forest");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
