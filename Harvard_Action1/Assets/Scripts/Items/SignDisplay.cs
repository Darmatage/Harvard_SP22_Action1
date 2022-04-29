using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDisplay : MonoBehaviour {

       public GameObject imageSign;
       public Text dialogueText;
       public string dialogue;
       public bool playerInRange = false;
	   //AudioManager audioManager;
	   public AudioSource bookSFX;

       void Start () {
            imageSign.SetActive(false);
			//audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
			bookSFX = GetComponent<AudioSource>();
	   }


          //James- updated this to no longer flicker when player stands on book
       void Update () {
            if (playerInRange){ //Input.GetKeyDown(KeyCode.Space) &&
               //     if (imageSign.activeInHierarchy){
               //          imageSign.SetActive(false);
               //     } else {
                        imageSign.SetActive(true);
						dialogueText.text = dialogue;     
                 //  }
            }
       }

       private void OnTriggerEnter2D(Collider2D other){
             if (other.CompareTag("Player")) {
                   playerInRange = true;
				   //audioManager.PlaySound("openbook");
				   bookSFX.Stop();
				   bookSFX.Play();
                   Debug.Log("Player in range");
                  }
             }
                        
       private void OnTriggerExit2D(Collider2D other){
             if (other.CompareTag("Player")) {
                   playerInRange = false;
                   imageSign.SetActive(false);
                   Debug.Log("Player left range");
                  }
             }
}