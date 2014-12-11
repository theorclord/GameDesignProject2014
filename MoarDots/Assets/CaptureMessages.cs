using UnityEngine;
using System.Collections;

public class CaptureMessages : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowForestMsg()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/forest_msg", typeof(Sprite)) as Sprite;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Transform>().localScale = new Vector3(2, 2, 1);
    }

    public void ShowMineMsg()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/mine_msg", typeof(Sprite)) as Sprite;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Transform>().localScale = new Vector3(2, 2, 1);
    }

    public void HideMsg()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
