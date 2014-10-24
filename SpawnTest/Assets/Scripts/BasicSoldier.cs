using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class BasicSoldier : MonoBehaviour
{
    
    //Char Stats NEW
    int health = 6;
    int damage = 3;

    int attackCD = 0; //new
    int cdCounter = 0; //new
    List<GameObject> targets = new List<GameObject>();
	
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
        attackCD = 30; //new - Add here to inheriting classes
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
        check(coll);
	}

    /*
    * CHANGED
    * 
    * */
    private void check(Collider2D coll)
    {
        float tempMS = movespeed;
        
        if (coll.gameObject.GetComponent<Unit>() != null)
        {
            if (coll.gameObject.GetComponent<Unit>().Owner != transform.parent.transform.gameObject.GetComponent<Unit>().Owner)
            {
                targets.Add(coll.gameObject.transform.FindChild("Collision").transform.gameObject);
                movespeed = 0;
                attack();
            }
  
            movespeed = tempMS;
        }
    }


	void move(float ms)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + ms);
	}

    /*
     * CHANGED
     */
	private void fight ()
	{
		// if this unit or the detected unit is in combat, the following code will be skipped
        if (!((combatState == true) || (CollChild.GetComponent<BasicSoldier>().combatState == true)))
        {
            CollChild.GetComponent<BasicSoldier>().CollChild = gameObject;
            CollChild.GetComponent<BasicSoldier>().combatState = true;
/*
            float me = drawRandomInt();

            float opp = drawRandomInt();

            if (me >= opp)
            {
                targets.Remove(CollChild);
                Destroy(CollChild.transform.parent.gameObject);
                combatState = false;
            }
            else
            {
                Destroy(transform.parent.gameObject);
                CollChild.GetComponent<BasicSoldier>().combatState = false;
            }
*/
            CollChild.GetComponent<BasicSoldier>().health = CollChild.GetComponent<BasicSoldier>().health - damage;

            if ( CollChild.GetComponent<BasicSoldier>().health <= 0)
            {
                targets.Remove(CollChild);
                Destroy(CollChild.transform.parent.gameObject);
                combatState = false;
            }
        }
	}

	float drawRandomInt ()
	{
		return Random.value;
	}

    //new
    void attack()
    {
        if (targets.Count > 0)
        {
            CollChild = targets[0];
            if (cdCounter > 0)
                attackCD--;
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

