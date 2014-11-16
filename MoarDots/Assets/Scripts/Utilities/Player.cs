using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;

public class Player {

    public List<string> Technology = new List<string>();

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

    public void setProperty(PropertyPair prop, float mod)
    {
        if (mod > 0) {
            switch (prop.Identifier)
            {
                case "Tech":
                    Technology.Add(prop.Val);
                    break;
                case "Income":
                    Income += int.Parse(prop.Val);
                    break;
            }
        }
        else
        {
            switch (prop.Identifier)
            {
                case "Tech":
                    Technology.Remove(prop.Val);
                    break;
                case "Income":
                    Income -= int.Parse(prop.Val);
                    break;
            }
        }
    }
}
