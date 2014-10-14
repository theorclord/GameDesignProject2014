using UnityEngine;
using System.Collections;

public class SpawnPair : MonoBehaviour {

    public int Path{
        get;
        set;
    }
    public GameObject UnitType
    {
        get;
        set;
    }

    public SpawnPair(int path, GameObject unitType)
    {
        this.Path = path;
        this.UnitType = unitType;

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
