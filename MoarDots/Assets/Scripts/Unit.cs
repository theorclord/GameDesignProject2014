﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    public int Movespeed;
    public float SpawnRate;

    public Player Owner { get; set; }

    private Vector2 velocity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setdirection(Vector2 dir)
    {
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