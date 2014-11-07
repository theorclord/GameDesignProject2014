using UnityEngine;
using System.Collections;

public class UnitType {

    public static const int HIGHER_TIER = 3;
    public static const int MIDDLE_TIER = 2;
    public static const int LOWER_TIER = 1;

	public string Name{ get; set; }
	public float Attack{ get; set; }
	public int Range{ get; set; }
	public int Movespeed{ get; set; }
	public float Health{ get; set; }
    public bool IsRanged { get; set; }
    public int Price { get; set; }
    public bool IsStructure { get; set; }
    public float Armour { get; set; } // Reduces incomming damage by %
    public float ArmourPen { get; set; } // ignores target armour by %
    public float AttackSpeed { get; set; } // hits per second
    public int Tier { get; set; } // determines the strength of the unit

    public UnitType(string name, float atk, float health, int range, int movespeed, bool isRanged, int price, bool isStructure
        ,float armour, float armourPen, float attackSpeed, int tier)
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
        Tier = tier;
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
