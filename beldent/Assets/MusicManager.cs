using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip coin;
	public AudioClip jump;
	public AudioClip crash;
	public AudioClip countdown;
	public AudioClip changeLane;

	public AudioClip music_interfaz;
	public AudioClip music_gamePlay;

	public AudioSource audioSourceFX1;
	public AudioSource audioSourceFX2;
	public AudioSource audioSource;

	void Start () {
		Events.OnMusic += OnMusic;
		Events.OnSoundFX += OnSoundFX;
		Events.OnMusic (1);
	}
	
	// Update is called once per frame
	void OnMusic (int id) {
		if (id == 0) {
			audioSource.Stop ();
			return;
		}
		if (id == 1)
			audioSource.clip = music_interfaz;
		else if (id == 2)
			audioSource.clip = music_gamePlay;
		audioSource.Play ();
	}
	void OnSoundFX(string name)
	{
		switch (name) {
		case "coin":
			audioSourceFX1.PlayOneShot(coin);break;
		case "jump":
			audioSourceFX1.PlayOneShot(jump);break;
		case "crash":
			audioSourceFX1.PlayOneShot(crash);break;
		case "countdown":
			audioSourceFX1.PlayOneShot(countdown);break;
		case "changeLane":
			audioSourceFX2.PlayOneShot(changeLane);break;
		}
	}
}
