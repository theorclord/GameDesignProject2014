using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public AudioClip zombie;// = (AudioClip)Resources.Load("Sounds/zombie-noice");
	public AudioClip pixie;// = (AudioClip)Resources.Load("Sounds/zombie-noice");
    public AudioClip z_archer;
    public AudioClip princess;
    public AudioClip evil_moose;
    public AudioClip centaur;

	public void playSound(int i){
		//Debug.Log (GameObject.Find ("SoundManager").audio.isPlaying);
		if (!(GameObject.Find ("SoundManager").audio.isPlaying)) {
			switch (i) {
			case 1:
				GameObject.Find ("SoundManager").audio.volume = 1.0f;
				GameObject.Find ("SoundManager").audio.clip = zombie;  // can change to different unit types
				GameObject.Find ("SoundManager").audio.Play();

				break;
            case 2:
                GameObject.Find("SoundManager").audio.volume = 1.0f;
                GameObject.Find("SoundManager").audio.clip = pixie;  // can change to different unit types
                GameObject.Find("SoundManager").audio.Play();
                break;
            case 3:
                GameObject.Find("SoundManager").audio.volume = 1.0f;
                GameObject.Find("SoundManager").audio.clip = z_archer;  // can change to different unit types
                GameObject.Find("SoundManager").audio.Play();
                break;
            case 4:
                GameObject.Find("SoundManager").audio.volume = 1.0f;
                GameObject.Find("SoundManager").audio.clip = princess;  // can change to different unit types
                GameObject.Find("SoundManager").audio.Play();
                break;
            case 5:
                GameObject.Find("SoundManager").audio.volume = 1.0f;
                GameObject.Find("SoundManager").audio.clip = evil_moose; // can change to different unit types
                GameObject.Find("SoundManager").audio.Play();
                break;
            case 6:
                GameObject.Find("SoundManager").audio.volume = 1.0f;
                GameObject.Find("SoundManager").audio.clip = centaur;  // can change to different unit types
                GameObject.Find("SoundManager").audio.Play();
                break;
			}
		}
	}
}
