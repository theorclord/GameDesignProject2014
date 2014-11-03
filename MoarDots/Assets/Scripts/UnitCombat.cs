using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class UnitCombat : MonoBehaviour
{
    int attackCD;
    int cdCounter = 0;
    List<GameObject> targets = new List<GameObject>();

	private GameObject CollChild;
    private Unit thisUnit; 
	
	// Use this for initialization
	void Start () {
        thisUnit = transform.parent.GetComponent<Unit>();
        attackCD = (int) (30 * (1 / thisUnit.AttackSpeed));
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
        //Checks if a unit enters detection range
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
            if (Vector3.Distance(transform.position, target.transform.position) * 100 < thisUnit.Range)
            {
                gameObject.transform.parent.rigidbody2D.velocity = new Vector2(0.0f, 0.0f);

                //deal damage to enemy
                //target.GetComponent<Unit>().Health -= thisUnit.Attack;
                target.GetComponent<Unit>().Health -= thisUnit.Attack * ((target.GetComponent<Unit>().Armour * (thisUnit.ArmourPen/100)) / 100);

                
                if (target.GetComponent<Unit>().Health <= 0 || targets.Count == 0)
                {
                    targets.Remove(target);
                    if (!target.GetComponent<Unit>().IsStructure)
                    {
                        Destroy(target);
                    }
                    transform.parent.GetComponent<Unit>().CombatState = false;
                    thisUnit.setdirection(thisUnit.CurrentDestination, true);
                }
            }
            else
            {
                thisUnit.setdirection(target.transform.position, true);
            }
        }
        else
        {
            if (thisUnit.CloseCombat)
            {
                //deal damage to enemy
                target.GetComponent<Unit>().Health -= thisUnit.Attack;
                //target.GetComponent<Unit>().Health -= thisUnit.Attack * ((target.GetComponent<Unit>().Armour * (thisUnit.ArmourPen / 100)) / 100);

                if (target.GetComponent<Unit>().Health <= 0 || targets.Count == 0)
                {
                    targets.Remove(target);
                    if (target.GetComponent<Unit>().Unittype.IsStructure)
                    {
                        target.GetComponent<Tower>().IsRuin = true;
                    }
                    else
                    {
                        Destroy(target);
                    }
                    transform.parent.GetComponent<Unit>().CombatState = false;
                    thisUnit.CloseCombat = false;
                    thisUnit.setdirection(thisUnit.CurrentDestination, true);
                }
            }
            else
            {
                thisUnit.setdirection(target.transform.position, true);
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
            thisUnit.setdirection(thisUnit.CurrentDestination, true);
        }
	}

}

