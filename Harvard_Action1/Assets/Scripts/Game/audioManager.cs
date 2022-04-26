using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip dieEnemySound, vomitSound, chompSound;
	public AudioSource audiosource;

	void Start() {
		chompSound = Resources.Load<AudioClip>("Audio/chomp");
		dieEnemySound = Resources.Load<AudioClip>("Audio/dieEnemy");
		vomitSound = Resources.Load<AudioClip>("Audio/vomit");

		audiosource = GameObject.FindWithTag("AudioSource").GetComponent<AudioSource>();
	}

	public void PlaySound (string clip) {
		switch (clip) {
			case "dieEnemy":
				audiosource.PlayOneShot(dieEnemySound);
				break;
			case "vomit":
				audiosource.PlayOneShot(vomitSound);
				break;
			case "chomp":
				audiosource.PlayOneShot(chompSound);
				break;
		}
	}
}
