using UnityEngine;
using System.Collections;

public class UnitType {


	public string Name{ get; set; }
	public int Attack{ get; set; }
	public int Range{ get; set; }
	public int Movespeed{ get; set; }
	public float Health{ get; set; }
    public bool IsRanged { get; set; }
    public int Price { get; set; }
    public bool IsStructure { get; set; }
    public float Armour { get; set; } // Reduces incomming damage by %
    public float ArmourPen { get; set; } // ignores target armour by %
    public float AttackSpeed { get; set; } // hits per second

    public UnitType(string name, int atk, float health, int range, int movespeed, bool isRanged, int price, bool isStructure
        ,float armour, float armourPen, float attackSpeed)
    {
        Name = name;
        Attack = atk;
        Health = health;
        Range = range;
        Movespeed = movespeed;
        IsRanged = isRanged;

        Price = price;
        IsStructure = isStructure;
        Armour = armour;
        ArmourPen = armourPen;
        AttackSpeed = attackSpeed;
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
