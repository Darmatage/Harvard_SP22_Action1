using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackShoot : MonoBehaviour{

      //public Animator animator;
      public Transform FirePoint;
      public GameObject projectilePrefab;
      public float projectileSpeed = 10f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
	  public bool FaceRight;
	  // public Rigidbody2D rig2;
	   //public static float runSpeed = 10f;

      void Start(){
		  GameObject player = GameObject.Find("Player");
		  PlayerController controller = player.GetComponent<PlayerController>();
		  FaceRight=controller.FaceRight;
           //animator = gameObject.GetComponentInChildren<Animator>();
      }

      void Update(){
           if (Time.time >= nextAttackTime){
                  //if (Input.GetKeyDown(KeyCode.Space))
                 if (Input.GetAxis("Attack") > 0){
                        playerFire();
                        nextAttackTime = Time.time + 1f / attackRate;
                  }
            }
      }

      void playerFire(){
            //animator.SetTrigger ("Fire");
			
			GameObject projectile = Instantiate(projectilePrefab, FirePoint.position, Quaternion.identity);
		


			
			Debug.Log("facing right" +FaceRight);
            //GameObject projectile = Instantiate(projectilePrefab, FirePoint.position, Quaternion.identity);
           // projectile.AddForce(fwd * projectileSpeed, ForceMode.Impulse);
      }
}