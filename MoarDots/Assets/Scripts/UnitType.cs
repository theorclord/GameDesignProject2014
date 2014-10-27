using UnityEngine;
using System.Collections;

public class UnitType : MonoBehaviour {


	public string name{ get; set; }
	public int atk{ get; set; }
	public int range{ get; set; }
	public int speed{ get; set; }
	public int health{ get; set; }


	// Use this for initialization
	void Start () {
		
	}

	public void setValues(string name, int atk,int health, int range, int speed){
		this.name = name;
		this.atk = atk;
		this.health = health;
		this.range = range;
		this.speed = speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
