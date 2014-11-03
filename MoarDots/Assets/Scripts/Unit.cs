using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    public int Movespeed;
    public float SpawnRate;
    
    public bool IsRanged = false;
    public bool CloseCombat = false;

    public bool CombatState
    {
        get;
        set;
    }

	public UnitType unittype;
    public int Health;
	public int Attack;
	public int Range;
	public string Name;

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
		unittype = ut;
		Movespeed = ut.Speed;
	    Attack = ut.Attack;
		Health = ut.Health;
		Name = ut.Name;
		Range = ut.Range;
        IsRanged = ut.IsRanged;
	}

	// Update is called once per frame
	void Update () {
        if (CloseCombat)
        {
            transform.rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
        }
	
	}

    public void setdirection(Vector2 dir)
    {
        if (!CombatState)
        {
            CurrentDestination = dir;
        }
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 newDirection = dir -pos;
        float normFac = 1/Mathf.Sqrt((newDirection.x * newDirection.x) + (newDirection.y * newDirection.y));
        newDirection = newDirection * normFac;
        transform.rigidbody2D.velocity = (newDirection / 10) * Movespeed;
    }

    public void updateUnit(string prop, float val)
    {
        switch (prop)
        {
            case "Movespeed":
                Movespeed += (int)val;
                break;
            case "SpawnRate":
                SpawnRate += val;
                break;
        }
    }
}
