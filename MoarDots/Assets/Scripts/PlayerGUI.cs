using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {

    
    public GameObject PlayerCastle;
    public Texture ZoombieBtnText;

    private SpawnPoint point;
    // Use this for initialization
	void Start () {
        point = PlayerCastle.GetComponent<SpawnPoint>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 100, 90), "Player GUI");

        GUI.Button(new Rect(30, 30, 60, 60), ZoombieBtnText);
    }




    private void buyUnit()
    {

    }
}
