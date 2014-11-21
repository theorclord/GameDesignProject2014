using UnityEngine;
using System.Collections;

public class UnitType {

    public const int HIGHER_TIER = 3;
    public const int MIDDLE_TIER = 2;
    public const int LOWER_TIER = 1;

    public int UnitTypeNumber { get; set; }
	public string Name{ get; set; }
	public float Attack{ get; set; }
	public int Range{ get; set; }
	public int Movespeed{ get; set; }
	public float Health{ get; set; }
    public bool IsRanged { get; set; }
    public int Price { get; set; }
    public bool IsStructure { get; set; }
    public float Armour { get; set; }
    public float ArmourPen { get; set; }
    public float AttackSpeed { get; set; }
    public int Tier { get; set; } // determines the strength of the unit

	GameObject SoundObject;

    public string Tech { get; set; }

    public UnitType(string name, float atk, float health, int range, int movespeed, bool isRanged, int price, bool isStructure
        ,float armour, float armourPen, float attackSpeed, int tier, string tech)
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

        Tech = tech;
		SoundObject = GameObject.Find ("SoundManager");
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
	public void playSound() //int unitTypeNumber
    {
        //Debug.Log ("playSound initialized");
        if (Random.Range(1, 100) >= 90)
        {
            //Debug.Log(name);
            SoundObject.GetComponent<SoundScript>().playSound(UnitTypeNumber);
           /* if (unitTypeNumber == "player")
            {
                //GameObject go = GameObject.Find("SoundManager");
                //Debug.Log (GameObject.Find ("SoundManager").audio.isPlaying + " - Player");
                SoundObject.GetComponent<SoundScript>().playSound(2);
            }
            else
            {
                //GameObject go = GameObject.Find("SoundManager");
                //Debug.Log (GameObject.Find ("SoundManager").audio.isPlaying + " - Enemy");
                SoundObject.GetComponent<SoundScript>().playSound(1);
            }*/
        }
    }
}
