using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public bool CloseCombat
    {
        get;
        set;
    }
    public bool CombatState
    {
        get;
        set;
    }

	public UnitType Unittype;
    public float Health;
	public int Attack;
	public int Range;
	public string Name;
    public float Movespeed;
    public bool IsRanged;
    public bool IsStructure;
    public float Armour;
    public float ArmourPen;
    public float AttackSpeed;

    public Player Owner { get; set; }

    public Vector2 CurrentDestination
    {
        get;
        set;
    }
    private Vector2 velocity;
	// Use this for initialization
	void Start () {
	
	}

	public void setUnitType(UnitType ut){
		Unittype = ut;
		Movespeed = ut.Movespeed;
	    Attack = ut.Attack;
		Health = ut.Health;
		Name = ut.Name;
		Range = ut.Range;
        IsRanged = ut.IsRanged;
        IsStructure = ut.IsStructure;
        Armour = ut.Armour;
        ArmourPen = ut.ArmourPen;
        AttackSpeed = ut.AttackSpeed;
	}

	// Update is called once per frame
	void Update () {
        /*
        if (CloseCombat)
        {
            transform.rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
        }
         */
	}

    public void setdirection(Vector2 dir, bool combat)
    {
        if (!combat)
        {
            CurrentDestination = dir;
        }
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 newDirection = dir -pos;
        float normFac = 1/Mathf.Sqrt((newDirection.x * newDirection.x) + (newDirection.y * newDirection.y));
        newDirection = newDirection * normFac;
        transform.rigidbody2D.velocity = (newDirection / 10) * Movespeed;
    }
}
