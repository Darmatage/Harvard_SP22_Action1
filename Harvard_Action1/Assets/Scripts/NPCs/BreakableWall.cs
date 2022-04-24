using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BreakableWall : MonoBehaviour {

      public Animator anim;
      //public GameObject ParticleVFX;
      //public AudioSource breakSFX;
      public int hitNum = 2; // how many times the object can be hit before it disappears.
      public GameObject boxColliderObj; // a child collider that can be turned off
      private Renderer myRend;
      private Color defaultColor;
	  public int halfwall=0;

      void Start(){
            anim = gameObject.GetComponentInChildren<Animator>();
            boxColliderObj.SetActive(true);
            myRend = gameObject.GetComponentInChildren<Renderer>();
            //defaultColor = myRend.material.color;
      }

      void Update(){
           /* if (hitNum == 2){
                  anim.SetBool ("wallHalf", false);
                  anim.SetBool ("wallGone", false);
            }
            else if (hitNum == 1){
                  anim.SetBool ("wallHalf", true);
                  anim.SetBool ("wallGone", false);
            }
            else if (hitNum == 0){
                  anim.SetBool ("wallHalf", false);
                  anim.SetBool ("wallGone", true);*/
                  boxColliderObj.SetActive (true);
            //}
			if(hitNum==2)
			{
				halfwall=1;
				//anim.SetBool("halfwall" , false);
			}
			else if(hitNum==1)
			{
				halfwall=0;
				boxColliderObj.SetActive (false);
			 Destroy (gameObject);
				//anim.SetBool("halfwall",true);
			}
			
      }

      public void wallDamage()
	  {
            // this is the function that the player attack script would access
            if (hitNum > 0) 
			{
			         
				  //if (!breakSFX.isPlaying){ breakSFX.Play(); 
			}
			//	if (hitNum == 2)
				// { 
					//	anim.SetTrigger ("cutFull"); 
			    // }
                //else if (hitNum == 1)
				 //{ 
					//	anim.SetTrigger ("cutHalf"); 
			    // }              
           // StartCoroutine(wallHitReturn());
            //}
			 if (hitNum==2)
			 {
				 Debug.Log("hitNum= "+hitNum);
			 //anim.setBool("halfwall", true);
			 //StartCoroutine(wallHitReturn());
			 
			 //boxColliderObj.SetActive (false);
			 //Destroy (gameObject);
			 halfwall=1;
			 }
			 //if(hitNum==1)
			 //{
			//	boxColliderObj.SetActive (false);
			 //Destroy (gameObject); 
			 //}
			 StartCoroutine(wallHitReturn());
	  }
	  

      IEnumerator wallHitReturn(){
            myRend.material.color = new Color(1.0f, 1.0f, 2.5f);
            yield return new WaitForSeconds(0.5f);
			Debug.Log("hitnum is -1"+hitNum);
            hitNum -= 1;
            myRend.material.color = defaultColor;
           // breakSFX.Stop();
      }

}