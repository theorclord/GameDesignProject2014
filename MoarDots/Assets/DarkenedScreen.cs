using UnityEngine;
using System.Collections;

public class DarkenedScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/blackened_screen" , typeof(Sprite)) as Sprite;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Transform>().localScale = new Vector3(2,2,1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EnableSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/blackened_screen", typeof(Sprite)) as Sprite;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DisableSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }
}
