using UnityEngine;
using System.Collections;

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
        
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override string ToString()
    {
        return Name + " " + playerColor;
    }
}
