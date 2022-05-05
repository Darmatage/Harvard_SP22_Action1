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
      public LayerMask enemyLayers;
	public LayerMask wallLayer;

      void Awake() {
            bars = GetComponent<PlayerBars>();
      }

      void Start(){
           //animator = gameObject.GetComponentInChildren<Animator>();
      }

      void Update(){
           if (Time.time >= nextAttackTime){
                 //if (Input.GetKeyDown(KeyCode.Space))
                 if (Input.GetAxis("Attack") > 0 && bars.vomit.GetValue() >= 0){ //temporarily made vomit unlimited again JEB
                        Attack();
                        nextAttackTime = Time.deltaTime + 1f / attackRate;
                 }
            }
      }

      void Attack() {
            bars.Vomit(1);

            //animator.SetTrigger ("Melee");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);
           
            foreach(Collider2D enemy in hitEnemies){
                  // Debug.Log("We hit " + enemy.name);
				  //GetComponent<AudioSource>().Play();
                  enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
            }

		Collider2D[] hitWalls = Physics2D.OverlapCircleAll(attackPt.position, attackRange, wallLayer);
           
		foreach(Collider2D wall in hitWalls) {
                  if (wall && wall.GetComponent<BreakableWall>()) {
                        wall.GetComponent<BreakableWall>().wallDamage();
                  }

			// Debug.Log("We hit " + wall.name);
            }
      }

      //NOTE: to help see the attack sphere in editor:
      void OnDrawGizmos() {
            // if (attackPt == null) {return;}
            // Gizmos.color = new Color(1, 1, 0, 0.75F);
            // Gizmos.DrawWireSphere(new Vector2(attackPt.position.x, attackPt.position.y), attackRange);
      }
}
