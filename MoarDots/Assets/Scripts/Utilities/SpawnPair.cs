using UnityEngine;
using System.Collections;

public class SpawnPair{

    public int Path{
        get;
        set;
    }
    public UnitType UnitType
    {
        get;
        set;
    }
    public int Amount
    {
        get;
        set;
    }

    public Player Owner
    {
        get;
        set;
    }

    public SpawnPair(int path, UnitType unitType, int amount, Player owner)
    {
        this.Path = path;
        this.UnitType = unitType;
        this.Amount = amount;
        this.Owner = owner;
    }
}
