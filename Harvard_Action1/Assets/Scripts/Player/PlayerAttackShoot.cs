using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackShoot : MonoBehaviour{

      public Animator animator;
      //AudioManager audioManager;
      private PlayerBars bars;
      PlayerController controller;
      public Transform FirePoint;
      public GameObject projectilePrefab;
      public float projectileSpeed = 10f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
	public bool FaceRight;
	  //public AudioSource playerShootSFX;
	  // public Rigidbody2D rig2;
	   //public static float runSpeed = 10f;
	public AudioSource vomitSFX;

      void Awake() {
            bars = GetComponent<PlayerBars>();
      }


      void Start(){
		GameObject player = GameObject.Find("Player");
		controller = player.GetComponent<PlayerController>();
            //audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
            animator = gameObject.GetComponentInChildren<Animator>();
      }

      void Update(){
           FaceRight = controller.FaceRight;

           if (Time.time >= nextAttackTime){
                  if (Input.GetAxis("Attack") > 0  && bars.vomit.GetValue() > 0) {
                        animator.SetTrigger ("Fire");
				vomitSFX.Play();
				playerFire();						
                        nextAttackTime = Time.time + 1f / attackRate;
                  }

                  if (Input.GetAxis("AltAttack") > 0  && bars.vomit.GetValue() > 0) {
                        animator.SetTrigger ("Fire");
				vomitSFX.Play();
				playerFire();						
                        nextAttackTime = Time.time + 1f / attackRate;
                  }
            }
      }

      void playerFire(){
            Quaternion targerRotation = Quaternion.identity;

            if (!FaceRight) {
                  targerRotation = Quaternion.Euler(new Vector3(0, -180, 0));
            }

		GameObject projectile = Instantiate(projectilePrefab, FirePoint.position, targerRotation);	
		
		//audioManager.PlaySound("vomit");
				//playerShootSFX.Play();
			//Debug.Log("IS facing right" +FaceRight);
            //GameObject projectile = Instantiate(projectilePrefab, FirePoint.position, Quaternion.identity);
           // projectile.AddForce(fwd * projectileSpeed, ForceMode.Impulse);
      }
}
