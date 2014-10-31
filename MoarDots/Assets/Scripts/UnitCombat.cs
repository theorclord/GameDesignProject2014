using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class UnitCombat : MonoBehaviour
{
    int attackCD = 30;
    int cdCounter = 0;
    List<GameObject> targets = new List<GameObject>();

	private GameObject CollChild;
    private GameObject thisUnit; 
	
	// Use this for initialization
	void Start () {
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
        Unit thisUnit = transform.parent.GetComponent<Unit>();
        if (thisUnit.IsRanged)
        {
            //TODO Ranged combat check
        }
        else
        {
            if (thisUnit.CloseCombat)
            {
                target.GetComponent<Unit>().Health -= thisUnit.Damage;


                if (target.GetComponent<Unit>().Health <= 0 || targets.Count == 0)
                {
                    targets.Remove(target);
                    Destroy(target);
                    transform.parent.GetComponent<Unit>().CombatState = false;
                    thisUnit.CloseCombat = false;
                    thisUnit.setdirection(thisUnit.CurrentDestination);
                }
            }
            else
            {
                thisUnit.setdirection(target.transform.position);
            }
        }
	}
	

	// Update is called once per frame
	void Update () {
        // remove empty targets
        while (targets.Count > 0 && targets[0] == null)
        {
            targets.RemoveAt(0);
        }

        Unit thisUnit = transform.parent.GetComponent<Unit>();
        // if there is more than one target available fight it
        if (targets.Count > 0)
        {
            thisUnit.CombatState = true;
            // checks for the attack cooldown
            //TODO should work on a timer
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
        else
        {
            thisUnit.CombatState = false;
            thisUnit.setdirection(thisUnit.CurrentDestination);
        }
	}

}

