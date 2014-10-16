using UnityEngine;
using System.Collections;
using System.Threading;

public class BasicSoldier : MonoBehaviour
{
	
	public float movespeed = 0.0f;
    public bool combatState
    {
        get;
        set;
    }
	public GameObject CollChild;
	
	// Use this for initialization
	void Start () {
        combatState = false;
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
        check(coll);
	}

    private void check(Collider2D coll)
    {
        float tempMS = movespeed;

        if (coll.gameObject.GetComponent<Unit>() != null)
        {
            if (coll.gameObject.GetComponent<Unit>().Owner != transform.parent.transform.gameObject.GetComponent<Unit>().Owner)
            {
                CollChild = coll.gameObject.transform.FindChild("Collision").transform.gameObject;
                movespeed = 0;
                fight();
            }
            movespeed = tempMS;
        }
    }


	void move(float ms)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + ms);
	}

	private void fight ()
	{
		// if this unit or the detected unit is in combat, the following code will be skipped
        if (!((combatState == true) || (CollChild.GetComponent<BasicSoldier>().combatState == true)))
        {
            CollChild.GetComponent<BasicSoldier>().CollChild = gameObject;
            CollChild.GetComponent<BasicSoldier>().combatState = true;

            float me = drawRandomInt();

            float opp = drawRandomInt();

            if (me >= opp)
            {
                Destroy(CollChild.transform.parent.gameObject);
                combatState = false;
            }
            else
            {
                Destroy(transform.parent.gameObject);
                CollChild.GetComponent<BasicSoldier>().combatState = false;
            }
        }
	}

	float drawRandomInt ()
	{
		return Random.value;
	}
	
	// Update is called once per frame
	void Update () {
	}

}

