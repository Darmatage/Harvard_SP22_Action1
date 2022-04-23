using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour{

      public int damage = 1;
      public GameObject hitEffectAnim;
      public float SelfDestructTime = 0.5f;
	  private GameHandler gameHandler;
	  public bool FaceRight;
	 

      void Start(){
           StartCoroutine(selfDestruct());
		   
      }

      //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
      void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
                  gameHandler.playerGetHit(damage);
            }
           if (other.gameObject.tag != "Player") {
			   Debug.Log("facing right" +FaceRight );
                  GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
                  Destroy (animEffect, 0.5f);
                  Destroy (gameObject);
            }
      }

      IEnumerator selfDestruct(){
            yield return new WaitForSeconds(SelfDestructTime);
            Destroy (gameObject);
      }
}