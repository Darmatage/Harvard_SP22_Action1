using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickHit : MonoBehaviour
{
	public PlayerBars health;
	 public int playercolor;
	 private GameHandler gameHandler;
	 public int damage = 10;
	 public GameObject hitVFX;
	 public CameraShake cameraShake;
	 public Animator brickanim;
	
    // Start is called before the first frame update
    void Start()
    {
		brickanim=gameObject.GetComponentInChildren<Animator>();
		GameObject myCamera = GameObject.FindWithTag("MainCamera");
       cameraShake = myCamera.transform.parent.GetComponent<CameraShake>();
	   // if (GameObject.FindGameObjectWithTag ("Player") != null) {
                  //   target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
              //}
			if (GameObject.FindWithTag ("Player") != null) {
                  health = GameObject.FindWithTag ("Player").GetComponent<PlayerBars> ();
			}
              if (GameObject.FindWithTag ("GameHandler") != null) {
                  gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
				  	
              }
    }

    // Update is called once per frame
    void Update()
    {
      playercolor=gameHandler.playercolor;  
    }
	public void OnCollisionEnter2D(Collision2D collision){
              if (collision.gameObject.tag == "Player") {
				  //Debug.Log(playercolor);
				  if(playercolor!=2){
					  
                    Debug.Log("brick is causing damage");
                    health.TakeDamage(damage);
					
				    //GameObject brickVFX = Instantiate(hitVFX, collision.gameObject.transform.position, Quaternion.identity);
				    brickanim.SetBool("BrickBreaking", true);
                    StartCoroutine(DestroyVFX(gameObject));
					cameraShake.ShakeCamera(0.15f,0.3f);
				  }
			  }
			  if(collision.gameObject.tag == "Ground"){
				  
				 // GameObject brickVFX = Instantiate(hitVFX, collision.gameObject.transform.position, Quaternion.identity);
				 brickanim.SetBool("BrickBreaking", true);
                StartCoroutine(DestroyVFX(gameObject));				  
			  }
	}
	IEnumerator DestroyVFX(GameObject gameObject){
          yield return new WaitForSeconds(0.5f);
          Destroy(gameObject);
          
          //Destroy(theEffect);
         // gameObject.GetComponent<AudioSource>().Stop();
	}
}
