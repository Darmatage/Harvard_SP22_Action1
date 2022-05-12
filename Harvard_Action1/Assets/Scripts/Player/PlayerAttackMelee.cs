using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour{

      //public Animator animator;
      public Transform attackPt;
	 // public Transform attackPtRight;
      public float attackRange = 3f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
      public int attackDamage = 40;
      private PlayerBars bars;
      private PlayerController controller;
      GameObject player;
      public LayerMask enemyLayers;
	public LayerMask wallLayer;
      private bool isAttacking = false;

      void Awake() {
            bars = GetComponent<PlayerBars>();
            controller = GetComponent<PlayerController>();
      }

      void Start(){
           //animator = gameObject.GetComponentInChildren<Animator>();
           player = GameObject.Find("Player");
      }

      void Update(){
           if (Time.time >= nextAttackTime){
                 //if (Input.GetKeyDown(KeyCode.Space))
                 if (Input.GetAxis("Attack") > 0 && bars.vomit.GetValue() >= 0){ //temporarily made vomit unlimited again JEB
                        Attack();
                        nextAttackTime = Time.deltaTime + 1f / attackRate;
                 }

                 if (Input.GetAxis("AltAttack") > 0 && bars.vomit.GetValue() >= 0){ //temporarily made vomit unlimited again JEB
                        AltAttack();
                 }
            }
      }

      float GetAttackX() {
            if (!controller) {
                  return 0;
            }

            if (controller.FaceRight) {
                  return attackPt.position.x - attackRange;
            }

            return attackPt.position.x + attackRange;
      }

      void Attack() {
            if (isAttacking) {
                  return;
            }

            isAttacking = true;

            bars.Vomit(1);

            //animator.SetTrigger ("Melee");
            // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

            Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(new Vector2(attackPt.position.x / 2, attackPt.position.y / 2), new Vector2(GetAttackX(), attackPt.position.y), enemyLayers);

            foreach(Collider2D enemy in hitEnemies) {
                  if (enemy != player) {
                        enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
                  }
            }

		Collider2D[] hitWalls = Physics2D.OverlapCircleAll(attackPt.position, attackRange, wallLayer);
           
		foreach(Collider2D wall in hitWalls) {
                  if (wall && wall.GetComponent<BreakableWall>()) {
                        wall.GetComponent<BreakableWall>().wallDamage();
                  }

			// Debug.Log("We hit " + wall.name);
            }

            isAttacking = false;
      }

      void AltAttack() {
            bars.Vomit(3);

            Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(new Vector2(attackPt.position.x / 2, attackPt.position.y / 2), new Vector2(GetAttackX() * 2, attackPt.position.y), enemyLayers);

            foreach(Collider2D enemy in hitEnemies) {
                  if (enemy != player) {
                        enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
                  }
            }
      }

      //NOTE: to help see the attack sphere in editor:
      void OnDrawGizmos() {
            // if (attackPt == null) {return;}
            Gizmos.color = Color.red;
            // Gizmos.DrawWireSphere(new Vector2(attackPt.position.x, attackPt.position.y), attackRange);
            float attackX = 0;

            if (controller) {
                  if (controller.FaceRight) {
                        attackX = GetAttackX() + 1;
                  }
                  else {
                        attackX = GetAttackX() - 1;
                  }
            }

            Gizmos.DrawWireCube(new Vector2(attackX, attackPt.position.y), new Vector2(attackRange, 1));
      }
}
