using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

       public Animator anim;
       public float speed = 4f;
       private Transform target;
       public int damage = 10;
	public CameraShake cameraShake;
       public int EnemyLives = 3;
       private GameHandler gameHandler;
	public PlayerBars health;
		
       public float attackRange = 1.5f;
       public float enemyPushBack = 1.5f;
       public bool isAttacking = false;
       private float scaleX;
	public int enemycolor;
	public int playercolor;
	   

       void Start () {
              cameraShake = GameObject.FindWithTag("CameraShake").GetComponent<CameraShake>();
              health = GameObject.FindWithTag("Player").GetComponent<PlayerBars>();
              anim = GetComponentInChildren<Animator> ();
              scaleX = gameObject.transform.localScale.x;
				//GameObject player=GameObject.Find("Player");
					//PlayerBars playerbars = player.GetComponent<PlayerBars>();
				
              if (GameObject.FindGameObjectWithTag ("Player") != null) {
                     target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
              }

              if (GameObject.FindWithTag ("GameHandler") != null) {
                  gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
				  	
              }
       }

       void Update () {
              float DistToPlayer = Vector3.Distance(transform.position, target.position);
			  playercolor=gameHandler.playercolor;
			if(enemycolor!=playercolor){
              if ((target != null) && (DistToPlayer <= attackRange)){
                     transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
                     if (isAttacking == false) {
                            //anim.SetBool("Walk", true);
                            //flip enemy to face player direction. Wrong direction? Swap the * -1.
                            if (target.position.x > gameObject.transform.position.x){
                                   gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
                            } else {
                                    gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
                            }
                     }
                     //else  { anim.SetBool("Walk", false);}
              }
               //else { anim.SetBool("Walk", false);}
			}
       }

         public void OnCollisionEnter2D(Collision2D collision){
              if (collision.gameObject.tag == "Player") {
			if(enemycolor != playercolor) {
                            isAttacking = true;
                             anim.SetBool("Fire", true);
				// Debug.LogFormat("enemymovehit player taking damage: {0}", damage);
                            //gameHandler.playerGetHit(damage);
				health.TakeDamage(damage);
				cameraShake.ShakeCamera(0.4f, 0.1f);
				float pushBack = 0f;

				if (collision.gameObject.transform.position.x > gameObject.transform.position.x){
                                   pushBack = enemyPushBack;
                            }
				else {
                                   pushBack = -enemyPushBack;
                            }

				collision.gameObject.transform.position = new Vector3(transform.position.x + pushBack, transform.position.y + 1, -1);
			}
                     else {
                            // Debug.LogFormat("Player made contact but no damage: {0} {1}", enemycolor, playercolor);
                     }
              }
       }

       public void OnCollisionExit2D(Collision2D collision){
              if (collision.gameObject.tag == "Player") {
                     isAttacking = false;
                     anim.SetBool("Fire", false);
              }
       }

       //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
}
