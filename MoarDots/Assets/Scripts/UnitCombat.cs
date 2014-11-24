using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class UnitCombat : MonoBehaviour
{
    private bool combatInitialized;
    private float localTimer;
    private List<GameObject> targets = new List<GameObject>();

	private GameObject CollChild;
    private Unit thisUnit; 
	private AudioClip audioClip;
	GameObject SoundObject;
	
	// Use this for initialization
	void Start () {
        thisUnit = transform.parent.GetComponent<Unit>();
		SoundObject = GameObject.Find ("SoundManager");
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
        Unit collUnit = coll.gameObject.GetComponent<Unit>();
        //Checks if a unit enters detection range
        if (collUnit != null)
        {
            if (collUnit.Owner != transform.parent.transform.gameObject.GetComponent<Unit>().Owner
                && collUnit.Health > 0)
            {
                targets.Add(coll.gameObject);
            }
        }
	}

    /// <summary>
    /// Function for determining damage to enemy target
    /// </summary>
    /// <param name="target">current target</param>
	private void fight (GameObject target)
	{
        if (Vector3.Distance(transform.position, target.transform.position) * 100 < thisUnit.Range)
        {
            gameObject.transform.parent.rigidbody2D.velocity = new Vector2(0.0f, 0.0f);

            //Execute combat
            executeCombat(target);
        }
        else
        {
            thisUnit.setdirection(target.transform.position, true);
        }
	}


	// Update is called once per frame
	void Update () {
        
        // remove empty targets
        while (targets.Count > 0 && (targets[0] == null || targets[0].GetComponent<Unit>().Health <0))
        {
            targets.RemoveAt(0);
        }

        Unit thisUnit = transform.parent.GetComponent<Unit>();
        // if there is more than one target available fight it
        if (targets.Count > 0)
        {
            thisUnit.CombatState = true;
            fight(targets[0]);
        }
        else
        {
            thisUnit.CombatState = false;
            thisUnit.setdirection(thisUnit.CurrentDestination, true);
        }
	}


    private void executeCombat(GameObject target)
    {
        if (!combatInitialized)
        {
            combatInitialized = true;
            localTimer = Time.time;
        }

        Unit targetUnit = target.GetComponent<Unit>();
        if (localTimer < Time.time)
        {
            float damage = thisUnit.Attack + thisUnit.Attack * 0.1f * Random.Range(-1, 2);
            damage += (thisUnit.UpgradeLevel * (thisUnit.Tier * 0.05f)) * damage; //Damage is increased based on upgrade level and class (class is constant)
            targetUnit.Health -= 1 +
                        (damage - targetUnit.Armour) / (1 + Mathf.Exp(-damage + targetUnit.Armour));
            localTimer += thisUnit.AttackSpeed;
        }
        if (targetUnit.Health <= 0 || targets.Count == 0)
		{
			// Find soundObject and play sound according to type, if random > 90

			targetUnit.getUnitType().playSound(); //.GetComponent<UnitType>().playSound(); // target.GetComponent<UnitType>().UnitTypeNumber
			//playSound(target.GetComponent<Unit>().Owner.Name);
			
            targets.Remove(target);
            if (target.GetComponent<Building>() != null)
            {
                target.GetComponent<Building>().setRuin(true, thisUnit.Owner);
            }
            else
            {

                Destroy(target);
            }
            transform.parent.GetComponent<Unit>().CombatState = false;
            combatInitialized = false;
            thisUnit.setdirection(thisUnit.CurrentDestination, true);
        }
    }
	void playSound(string name){
		//Debug.Log ("playSound initialized");
		if (Random.Range(1,100) >= 90) {
			//Debug.Log(name);
			if(name == "player"){
				//GameObject go = GameObject.Find("SoundManager");
				//Debug.Log (GameObject.Find ("SoundManager").audio.isPlaying + " - Player");
				SoundObject.GetComponent<SoundScript>().playSound(2);
			}else{
				//GameObject go = GameObject.Find("SoundManager");
				//Debug.Log (GameObject.Find ("SoundManager").audio.isPlaying + " - Enemy");
				SoundObject.GetComponent<SoundScript>().playSound(1);
			}
		}
	}
}

