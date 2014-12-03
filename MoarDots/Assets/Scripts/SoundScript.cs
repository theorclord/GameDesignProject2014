using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public AudioClip zombie;// = (AudioClip)Resources.Load("Sounds/zombie-noice");
	public AudioClip pixie;// = (AudioClip)Resources.Load("Sounds/zombie-noice");
    public AudioClip z_archer;
    public AudioClip princess;
    public AudioClip evil_moose;
    public AudioClip unicorn;
	public AudioClip soldier;
	public AudioClip hound;
	public AudioClip pugry;

	public void playSound(int i){
		//Debug.Log (GameObject.Find ("SoundManager").audio.isPlaying);
		GameObject newSound = Instantiate(Resources.Load("Prefab/SoundManager", typeof(GameObject)) as GameObject,
		                                  new Vector3(0, 0), Quaternion.identity) as GameObject;

		//if (!(GameObject.Find ("SoundManager").audio.isPlaying)) {
			switch (i) {
			case 1:
				newSound.audio.volume = 5.0f;
				newSound.audio.clip = zombie;
				newSound.audio.Play();
				StartCoroutine(TestCoroutine(newSound));

				/*GameObject.Find ("SoundManager").audio.volume = 2.0f;
				GameObject.Find ("SoundManager").audio.clip = zombie;  // can change to different unit types
				GameObject.Find ("SoundManager").audio.Play();*/

				break;
            case 2:
				newSound.audio.volume = 2.0f;
				newSound.audio.clip = pixie;
				newSound.audio.Play();
				StartCoroutine(TestCoroutine(newSound));
                /*GameObject.Find("SoundManager").audio.volume = 2.0f;
                GameObject.Find("SoundManager").audio.clip = pixie;  // can change to different unit types
                GameObject.Find("SoundManager").audio.Play();*/
                break;
            case 3:
				newSound.audio.volume = 2.0f;
				newSound.audio.clip = soldier;
				newSound.audio.Play();
				StartCoroutine(TestCoroutine(newSound));
                
                break;
            case 4:
				newSound.audio.volume = 2.0f;
				newSound.audio.clip = princess;
				newSound.audio.Play();
				StartCoroutine(TestCoroutine(newSound));
                
                break;
            case 5:
				newSound.audio.volume = 2.0f;
				newSound.audio.clip = evil_moose;
				newSound.audio.Play();
				StartCoroutine(TestCoroutine(newSound));
                
                break;
		case 6:
			newSound.audio.volume = 2.0f;
			newSound.audio.clip = unicorn;
			newSound.audio.Play();
			StartCoroutine(TestCoroutine(newSound));
			break;
		case 7:
			newSound.audio.volume = 5.0f;
			newSound.audio.clip = hound;
			newSound.audio.Play();
			StartCoroutine(TestCoroutine(newSound));
			break;
		case 8:
			newSound.audio.volume = 2.0f;
			newSound.audio.clip = pugry;
			newSound.audio.Play();
			StartCoroutine(TestCoroutine(newSound));
			break;
		}
		//}
	}
	IEnumerator TestCoroutine(GameObject go)
	{
		yield return new WaitForSeconds(3);
		Destroy (go);
	}
}
