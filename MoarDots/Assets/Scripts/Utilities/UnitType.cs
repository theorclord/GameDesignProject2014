using UnityEngine;
using System.Collections;

public class UnitType {


	public string Name{ get; set; }
	public int Attack{ get; set; }
	public int Range{ get; set; }
	public int Movespeed{ get; set; }
	public int Health{ get; set; }
    public bool IsRanged { get; set; }

    public UnitType(string name, int atk, int health, int range, int movespeed, bool isRanged)
    {
        Name = name;
        Attack = atk;
        Health = health;
        Range = range;
        Movespeed = movespeed;
        IsRanged = isRanged;
    }

    public void updateUnitType(string prop, float val)
    {
        switch (prop)
        {
            case "Movespeed":
                Movespeed += (int)val;
                break;
            case "Health":
                Health += (int)val;
                break;
            case "Attack":
                Attack += (int)val;
                break;
        }
    }
}
