using UnityEngine;
using System.Collections;

public class Castle : MonoBehaviour {

    private int soldierCount;
    private bool owned;
	// Use this for initialization
	void Start () {
        owned = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (soldierCount >= 1 && !owned)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("PlayerCastleCircle", typeof(Sprite)) as Sprite;
            owned = true;
            Debug.Log("City conquered");
        }
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        soldierCount++;
        Destroy(coll.gameObject);
        
    }
}
