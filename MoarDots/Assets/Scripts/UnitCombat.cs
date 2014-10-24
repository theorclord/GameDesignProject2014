using UnityEngine;
using System.Collections;
using System.Threading;

public class UnitCombat : MonoBehaviour
{
    public bool combatState
    {
        get;
        set;
    }
	private GameObject CollChild;
	
	// Use this for initialization
	void Start () {
        combatState = false;
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
        if (coll.gameObject.GetComponent<Unit>() != null)
        {
            if (coll.gameObject.GetComponent<Unit>().Owner != transform.parent.transform.gameObject.GetComponent<Unit>().Owner)
            {
                CollChild = coll.gameObject.transform.FindChild("Collision").transform.gameObject;
                fight();
            }
        }
	}


	void move(float ms)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + ms);
	}

	private void fight ()
	{
		// if this unit or the detected unit is in combat, the following code will be skipped
        if (!((combatState == true) || (CollChild.GetComponent<UnitCombat>().combatState == true)))
        {
            CollChild.GetComponent<UnitCombat>().CollChild = gameObject;
            CollChild.GetComponent<UnitCombat>().combatState = true;

            float me = Random.value;

            float opp = Random.value;

            if (me >= opp)
            {
                Destroy(CollChild.transform.parent.gameObject);
                combatState = false;
            }
            else
            {
                Destroy(transform.parent.gameObject);
                CollChild.GetComponent<UnitCombat>().combatState = false;
            }
        }
	}
	

	// Update is called once per frame
	void Update () {
	}

}

