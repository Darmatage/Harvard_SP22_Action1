using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour {
       private Renderer rend;
       public Animator anim;
       public GameObject healthLoot;
       public int maxHealth = 10;
       public int currentHealth;
       public CameraShake cameraShake;
       public AudioSource enemyDieSFX;

       void Start(){
              rend = GetComponentInChildren<Renderer>();
              anim = GetComponentInChildren<Animator>();
              currentHealth = maxHealth;
              cameraShake = GameObject.FindWithTag("CameraShake").GetComponent<CameraShake>();
       }

       public void TakeDamage(int damage){
              currentHealth -= damage;
              //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
              //StartCoroutine(ResetColor());
              //anim.SetTrigger ("Hurt");
              Debug.LogFormat("Dragon Health: {0} {1}", damage, currentHealth);
              if (currentHealth <= 0){
				 // anim.SetBool ("isDead", true);
				  
			Die();
			//GetComponent<AudioSource>().Play();
			enemyDieSFX.Play();
					
		       //Debug.Log("camera shake");
              }
       }

       void Die(){
		   
              Instantiate (healthLoot, transform.position, Quaternion.identity);
              
			  //Debug.Log("isDead=",anim);
              GetComponent<Collider2D>().enabled = false;
			  cameraShake.ShakeCamera(0.15f,0.3f);
              StartCoroutine(Death());
       }

       IEnumerator Death(){
              yield return new WaitForSeconds(1f);
              //Debug.Log("You Killed a baddie. You deserve loot!");
			  Destroy(gameObject);
       }

       IEnumerator ResetColor(){
              yield return new WaitForSeconds(1f);
              rend.material.color = Color.white;
       }
}
