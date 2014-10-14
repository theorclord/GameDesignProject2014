using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject MouseCursor;
    private GameObject mouse;

	// Use this for initialization
	void Start () {
        mouse = Instantiate(MouseCursor, Input.mousePosition, Quaternion.identity) as GameObject;
	
	}
	
	// Update is called once per frame
	void Update () {
        /*
        float x = 10.0f / 1366.0f * Input.mousePosition.x;
        float y = 10.0f / 768.0f * Input.mousePosition.y-1.5f;
        mouse.transform.position = new Vector3(x,y);
         */
        Vector3 camCoord =Camera.main.ScreenToWorldPoint( Input.mousePosition);
        mouse.transform.position = new Vector3(camCoord.x, camCoord.y);
	}
}
