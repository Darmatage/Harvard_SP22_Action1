using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickHit : MonoBehaviour
{
	public PlayerBars health;
	 public int playercolor;
	 private GameHandler gameHandler;
	 public int damage = 10;
	
    // Start is called before the first frame update
    void Start()
    {
       
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
        
    }
	public void OnCollisionEnter2D(Collision2D collision){
              if (collision.gameObject.tag == "Player") {
				  if(playercolor!=2){
                     Debug.Log("brick is causing damage");
                      health.TakeDamage(damage);
					 //cameraShake.ShakeCamera(0.15f,0.3f);
				  }
			  }
			  if(collision.gameObject.tag == "Ground"){
				  Destroy(gameObject);
			  }
	}
}
