using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorExit_1 : MonoBehaviour{

      public string NextLevel = "MainMenu";

      // public void Start(){
      //       audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
      // }         
      public void OnTriggerEnter2D(Collider2D other){
            // AudioManager audioManager;
            // audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
		//   audioManager.PlaySound("opendoor");
            if (other.gameObject.tag == "Player"){
                  SceneManager.LoadScene(NextLevel);
            }
      }
}
