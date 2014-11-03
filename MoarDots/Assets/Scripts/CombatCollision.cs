using UnityEngine;
using System.Collections;

public class CombatCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Unit thisUnit = gameObject.transform.parent.GetComponent<Unit>();
        Unit collUnit = coll.gameObject.transform.parent.GetComponent<Unit>();
        if (collUnit != null)
        {
            if (collUnit.Owner.Name != thisUnit.Owner.Name)
            {
                thisUnit.CloseCombat = true;
                gameObject.transform.parent.rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
                //Debug.Log("you woot mate");
            }
        }
    }
}
