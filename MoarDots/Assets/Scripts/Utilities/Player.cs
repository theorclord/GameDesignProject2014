using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

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

    public List<GameObject> unitList
    {
        get;
        private set;
    }

    public List<UnitType> unitTypeList
    {
        get;
        private set;
    }

    public Player()
    {
        unitList = new List<GameObject>();
        unitTypeList = new List<UnitType>();
    }

    public override string ToString()
    {
        return Name + " " + playerColor;
    }
}
