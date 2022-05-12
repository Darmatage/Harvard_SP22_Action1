using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit_1 : MonoBehaviour{

  public string nextLevel = "scene4_RooftopJellyDragonBoss_Shared";
	public AudioSource doorSFX;
	public GameHandler gameHandler;
	public int playercolor;

	public void Start(){
		//       audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
		doorSFX = GetComponent<AudioSource>();
		gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
	}         
	public void Update() {
		playercolor=gameHandler.playercolor;
	}
	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && gameHandler.canOpenDoor) {
			SceneManager.LoadScene(nextLevel);			
		}	
	}	  
}
