using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorExit_1 : MonoBehaviour{

AudioManager audioManager;

public void Start(){
		audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
}
      public string NextLevel = "MainMenu";

      public void OnTriggerEnter2D(Collider2D other){
		  audioManager.PlaySound("opendoor");
            if (other.gameObject.tag == "Player"){
                  SceneManager.LoadScene (NextLevel);
            }
      }

}
