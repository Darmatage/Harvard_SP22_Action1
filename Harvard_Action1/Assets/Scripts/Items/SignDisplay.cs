using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDisplay : MonoBehaviour {

       public GameObject imageSign;
       public Text dialogueText;
       public string dialogue;
       public bool playerInRange = false;

       void Start () {
              imageSign.SetActive(false);
       }

       void Update () {
            if (Input.GetKeyDown(KeyCode.Space) && playerInRange){
                   if (imageSign.activeInHierarchy){
                        imageSign.SetActive(false);
                   } else {
                        imageSign.SetActive(true);
						dialogueText.text = dialogue;
                        
                   }
            }
       }

       private void OnTriggerEnter2D(Collider2D other){
             if (other.CompareTag("Player")) {
                   playerInRange = true;
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