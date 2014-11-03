using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
    public int Income
    {
        get;
        set;
    }
    public int Resources
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }
    public Color playerColor
    {
        get;
        set;
    }

    public List<UnitType> unitTypeList
    {
        get;
        private set;
    }

    public Player()
    {
        unitTypeList = new List<UnitType>();
    }

    public override string ToString()
    {
        return Name + " " + playerColor;
    }
}
