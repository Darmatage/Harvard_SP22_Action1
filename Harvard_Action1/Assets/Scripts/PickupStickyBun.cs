using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickupStickyBun : MonoBehaviour{
	
	public CameraShake cameraShake;
	      public GameHandler gameHandler;
      //public playerVFX playerPowerupVFX;
      public bool isStickyBun = true;
      public bool isSpeedBoostPickUp = false;
	public Animator playerAnimator;
	
		
      public int healthBoost = 50;
      public float speedBoost = 2f;
      public float speedTime = 2f;

      void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
			
			GameObject myCamera = GameObject.FindWithTag("MainCamera");
			cameraShake = myCamera.transform.parent.GetComponent<CameraShake>();
			
			playerAnimator = GameObject.Find("PlayerArt").GetComponent<Animator>();
      }

      public void OnTriggerEnter2D (Collider2D other){
                
            if (other.gameObject.tag == "Player"){
                  GetComponent<Collider2D>().enabled = false;
                  GetComponent<AudioSource>().Play();
				  cameraShake.ShakeCamera(0.15f,0.1f);
                  StartCoroutine(DestroyThis());
					
					
                  if (isStickyBun == true) {
					  // Debug.Log("pickup playercolor to 3");
                       playerAnimator.SetInteger("PlayerColor",3);
					   gameHandler.playercolor=3;
					   
					   
			// gameHandler.playerGetHit(healthBoost * -1);
                        //playerPowerupVFX.powerup();
                  }

                  if (isSpeedBoostPickUp == true) {
                       // other.gameObject.GetComponent<PlayerMovement>().speedBoost(speedBoost, speedTime);
                        //playerPowerupVFX.powerup();
                  }
            }
      }

      IEnumerator DestroyThis(){
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
      }

}