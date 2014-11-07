using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    public string Name
    {
        get;
        set;
    }

    public Player Owner
    {
        get;
        set;
    }

    public bool IsRuin
    {
        get;
        private set;
    }

    public string BuildingSprite
    {
        get;
        set;
    }

    public string RuinSprite
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

    public void setRuin(bool ruin, Player owner)
    {
        Owner = owner;
        gameObject.GetComponent<Unit>().Owner = Owner;
        IsRuin = ruin;
        if (IsRuin)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(RuinSprite, typeof(Sprite)) as Sprite;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(BuildingSprite, typeof(Sprite)) as Sprite;
        }
    }

}
