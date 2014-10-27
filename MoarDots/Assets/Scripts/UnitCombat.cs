using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class UnitCombat : MonoBehaviour
{
    public int health = 0; //new
    public int damage = 0; //new

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
                targets.Add(coll.gameObject.transform.FindChild("Collision").transform.gameObject);
                Debug.Log("target added");
            }
        }
	}

	private void fight ()
	{
		// if this unit or the detected unit is in combat, the following code will be skipped
        if (!((combatState == true) || (CollChild.GetComponent<UnitCombat>().combatState == true)))
        {
            CollChild.GetComponent<UnitCombat>().CollChild = gameObject;
            CollChild.GetComponent<UnitCombat>().combatState = true;

            CollChild.GetComponent<UnitCombat>().health = CollChild.GetComponent<UnitCombat>().health - damage;
            Debug.Log("Damage done");
            Debug.Log("Health is " + CollChild.GetComponent<UnitCombat>().health);

            if (CollChild.GetComponent<UnitCombat>().health <= 0)
            {
                targets.Remove(CollChild);
                Destroy(CollChild.transform.parent.gameObject);
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
            CollChild = targets[0];
            if (cdCounter > 0)
            {
                cdCounter--;
                Debug.Log("Thre remaining CD is: " + cdCounter);
            }
            else
            {
                fight();
                cdCounter = attackCD;
            }
        }
    }
	

	// Update is called once per frame
	void Update () {
        attack(); //new
	}

}

