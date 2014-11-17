using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    private float max = 50f;
    private float min = -50f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float speed = 3f;
        transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min, max), transform.position.y, Mathf.Clamp(transform.position.z, min, max));
 
        //Change the '-10f' and '10f' to different numbers in order to limit the position to that number. Remember to check best answer :)
	
	}
}
