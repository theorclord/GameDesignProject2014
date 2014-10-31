using UnityEngine;
using System.Collections;

public class UnitType {


	public string Name{ get; set; }
	public int Attack{ get; set; }
	public int Range{ get; set; }
	public int Speed{ get; set; }
	public int Health{ get; set; }
    public bool IsRanged { get; set; }

	public void setValues(string name, int atk,int health, int range, int speed, bool isRanged){
		Name = name;
		Attack = atk;
		Health = health;
		Range = range;
		Speed = speed;
        IsRanged = isRanged;

	}
}
