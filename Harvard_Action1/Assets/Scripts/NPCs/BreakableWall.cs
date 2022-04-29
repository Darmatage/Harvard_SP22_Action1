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
	public CameraShake cameraShake;
	//AudioManager audioManager;
	public AudioSource wallSFX;

	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
		boxColliderObj.SetActive(true);
		myRend = gameObject.GetComponentInChildren<Renderer>();
		//audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        //defaultColor = myRend.material.color;
		wallSFX = GetComponent<AudioSource>();
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
			/*if(hitNum==2)
			{
				//halfwall=1;
				//Debug.Log("halfwall= "+halfwall);
				//anim.SetBool("halfwall" , false);
			}
			else if(hitNum==1)
			{
				
				halfwall=0;
				
				//anim.SetBool("halfwall",true);
			}
			else if(hitNum==0)
			{
				//halfwall=0;
				
			}*/
			
	}

	public void wallDamage(){
            // this is the function that the player attack script would access
		boxColliderObj.SetActive (false);
		Destroy (gameObject);
		//audioManager.PlaySound("wallbreak");
		wallSFX.Play();
		cameraShake.ShakeCamera(0.15f,0.3f);

			/*if (hitNum > 0) 
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
				 //Debug.Log("hitNum= "+hitNum);
				 //halfwall=1;
			 //anim.setBool("halfwall", true);
			 //StartCoroutine(wallHitReturn());
			 
			 //boxColliderObj.SetActive (false);
			 //Destroy (gameObject);
			 //halfwall=1;
			 }
			 //if(hitNum==1)
			 //{
			//	boxColliderObj.SetActive (false);
			 //Destroy (gameObject); 
			 //}
			 StartCoroutine(wallHitReturn());*/
	  }
	  

      IEnumerator wallHitReturn(){
            myRend.material.color = new Color(1.0f, 1.0f, 2.5f);
            yield return new WaitForSeconds(0.5f);
			//Debug.Log("hitnum is -1"+hitNum);
            hitNum --;
            myRend.material.color = defaultColor;
           // breakSFX.Stop();
      }

}