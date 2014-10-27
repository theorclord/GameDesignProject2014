using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class UnitCombat : MonoBehaviour
{
    

    int attackCD = 30; //new
    int cdCounter = 0; //new
    List<GameObject> targets = new List<GameObject>();

    public bool combatState
    {
        get;
        set;
    }
	private GameObject CollChild;
    private GameObject thisUnit; 
	
	// Use this for initialization
	void Start () {
        combatState = false;
        thisUnit = transform.parent.gameObject;
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
        if (coll.gameObject.GetComponent<Unit>() != null)
        {
            if (coll.gameObject.GetComponent<Unit>().Owner != transform.parent.transform.gameObject.GetComponent<Unit>().Owner)
            {
                targets.Add(coll.gameObject);
            }
        }
	}

	private void fight (GameObject target)
	{
		// if this unit or the detected unit is in combat, the following code will be skipped
        if (combatState != true)
        {
            target.GetComponent<Unit>().health -= gameObject.transform.parent.GetComponent<Unit>().damage;
            Debug.Log("health is " + target.GetComponent<Unit>().health);


            if (target.GetComponent<Unit>().health <= 0)
            {
                targets.Remove(target);
                Destroy(target);
                combatState = false;
            }
        }
	}

    //new
    void attack()
    {
        while (targets.Count>0 && targets[0] == null)
        {
            targets.RemoveAt(0);
        }

        if (targets.Count > 0)
        {
            if (cdCounter > 0)
            {
                cdCounter--;
            }
            else
            {
                fight(targets[0]);
                cdCounter = attackCD;
            }
        }
    }
	

	// Update is called once per frame
	void Update () {
        attack(); //new
	}

}

