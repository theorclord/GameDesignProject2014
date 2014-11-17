using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public AudioClip zombie;// = (AudioClip)Resources.Load("Sounds/zombie-noice");
	public AudioClip pixie;// = (AudioClip)Resources.Load("Sounds/zombie-noice");

	public void playSound(int i){
		//Debug.Log (GameObject.Find ("SoundManager").audio.isPlaying);
		if (!(GameObject.Find ("SoundManager").audio.isPlaying)) {
			switch (i) {
			case 1:
				GameObject.Find ("SoundManager").audio.volume = 0.3f;
				GameObject.Find ("SoundManager").audio.clip = zombie;
				GameObject.Find ("SoundManager").audio.Play();

				break;
			case 2:
				GameObject.Find ("SoundManager").audio.volume = 1.0f;
				GameObject.Find ("SoundManager").audio.clip = pixie;
				GameObject.Find ("SoundManager").audio.Play();
				break;
			}
		}
	}
}
