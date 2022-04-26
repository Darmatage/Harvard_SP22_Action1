using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip dieEnemySound, vomitSound, chompSound, jumpSound, wallbreakSound, openbookSound, opendoorSound;
	public AudioSource audiosource;

	void Start() {
		chompSound = Resources.Load<AudioClip>("Audio/chomp");
		dieEnemySound = Resources.Load<AudioClip>("Audio/dieEnemy");
		vomitSound = Resources.Load<AudioClip>("Audio/vomit");
		jumpSound = Resources.Load<AudioClip>("jump");
		wallbreakSound = Resources.Load<AudioClip>("wallbreak");
		openbookSound = Resources.Load<AudioClip>("openbook");
		opendoorSound = Resources.Load<AudioClip>("opendoor");
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
			case "jump":
				audiosource.PlayOneShot(jumpSound);
				break;
			case "wallbreak":
				audiosource.PlayOneShot(wallbreakSound);
				break;
			case "openbook":
				audiosource.PlayOneShot(openbookSound);
				break;
			case "opendoor":
				audiosource.PlayOneShot(opendoorSound);
				break;
		}
	}
}
