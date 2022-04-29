using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorExit_1 : MonoBehaviour{

    public string nextLevel = "MainMenu";
	public AudioSource doorSFX;

    public void Start(){
      //       audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
		doorSFX = GetComponent<AudioSource>();
	}         
      
	  public void OnTriggerEnter2D(Collider2D other){
            // AudioManager audioManager;
            // audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
			//   audioManager.PlaySound("opendoor");
            if (other.gameObject.tag == "Player"){
				doorSFX.Play();
				StartCoroutine(GoNextLevel());   
            }
      }
	  
	IEnumerator GoNextLevel(){
		yield return new WaitForSeconds(1f); 
		SceneManager.LoadScene(nextLevel);
	}  
	 
}
