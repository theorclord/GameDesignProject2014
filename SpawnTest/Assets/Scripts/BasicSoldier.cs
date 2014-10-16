using UnityEngine;
using System.Collections;
using System.Threading;

public class BasicSoldier : MonoBehaviour
{
	
/* MSV2	private Vector3 direction; */
	public float movespeed = 0.0f;
	public bool combatState = false;
	public GameObject opponent;
	
	// Use this for initialization
	void Start () {
/* MSV2	direction = transform.position; */
	}
	
	/*	void OnCollisionEnter2D(Collision2D coll) {
	//	if (coll.gameObject.tag == "soldier_two")
			Debug.Log("soldier_one has attacked - Detect");
	//	gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("soldier_form_c2", typeof(Sprite)) as Sprite;
	} */
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "soldier_two")
			Debug.Log("soldier_one has attacked - Trigger");	
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("soldier_form_c2", typeof(Sprite)) as Sprite;
		opponent = coll.gameObject;
		float tempMS = movespeed;
		movespeed = 0;
		fight ();
//		Thread.Sleep (1500);
		movespeed = tempMS;

		
		/* MSV2	direction = new Vector3(coll.transform.position.x, coll.transform.position.y);
		movespeed = 0; */
	}
	
	void die()
	{
		Object.Destroy (gameObject, 0.0F);
	}
	/* MSV2
	private void move(Vector3 direction)
	{
		direction.y += movespeed;
		Vector3.MoveTowards (transform.position, direction, 0.5f);
	}
*/
	
	void move(float ms)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + ms);
	}

	void fight ()
	{
		// if this unit or the detected unit is in combat, the following code will be skipped
		if ((combatState == true) || (opponent.GetComponent<BasicSoldier> ().combatState == true))
						;
				else {

						opponent.GetComponent<BasicSoldier> ().opponent = gameObject;
						opponent.GetComponent<BasicSoldier> ().combatState = true;

						float me = drawRandomInt ();
						Debug.Log (gameObject.tag + " has drawn " + me);

						float opp = drawRandomInt ();
						Debug.Log (opponent.tag + " has drawn " + opp);

						if (me >= opp) {
								Debug.Log (opponent.tag + " has died!");
								opponent.GetComponent<BasicSoldier> ().die ();
						} else {
								Debug.Log (gameObject.tag + " has died!");
								die ();
						}
				}
	}

	float drawRandomInt ()
	{
		return Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		/* MSV2		move (direction); */
		move (movespeed);
	}

}

