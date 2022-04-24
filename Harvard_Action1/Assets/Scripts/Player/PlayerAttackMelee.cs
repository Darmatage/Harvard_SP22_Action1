using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour{

      //public Animator animator;
      public Transform attackPt;
      public float attackRange = 0.5f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
      public int attackDamage = 40;
      public LayerMask enemyLayers;
	  public LayerMask wallLayer;

      void Start(){
           //animator = gameObject.GetComponentInChildren<Animator>();
      }

      void Update(){
           if (Time.time >= nextAttackTime){
                  //if (Input.GetKeyDown(KeyCode.Space))
                 if (Input.GetAxis("Attack") > 0){
                        Attack();
                        nextAttackTime = Time.time + 1f / attackRate;
                  }
            }
      }

      void Attack(){
            //animator.SetTrigger ("Melee");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);
           
            foreach(Collider2D enemy in hitEnemies){
                  Debug.Log("We hit " + enemy.name);
                  enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
            }
			Collider2D[] hitWalls = Physics2D.OverlapCircleAll(attackPt.position, attackRange, wallLayer);
           
          foreach(Collider2D wall in hitWalls){
                  
                  wall.GetComponent<BreakableWall>().wallDamage();
				  Debug.Log("We hit " + wall.name);
          }
      }

      //NOTE: to help see the attack sphere in editor:
      void OnDrawGizmosSelected(){
           if (attackPt == null) {return;}
            Gizmos.DrawWireSphere(attackPt.position, attackRange);
      }
}