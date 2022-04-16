using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

      // TODO: public Animator animator;
      public Rigidbody2D rb2D;
      private bool FaceRight = true; // Which way is the player facing?
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      public bool isAlive = true;
      // TODO: public AudioSource WalkSFX;
      private Vector3 hMove;

      void Start(){
           //animator = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

      void Update(){
            hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);

            // When player is alive, move
            if (isAlive == true) {
                transform.position = transform.position + hMove * runSpeed * Time.deltaTime;
            }

            if ((hMove.x <0 && !FaceRight) || (hMove.x >0 && FaceRight)) {
                playerTurn();
            }
      }

      void FixedUpdate(){
            // Changes velocity on hills and slides
            if (hMove.x == 0) {
                rb2D.velocity = new Vector2(rb2D.velocity.x / 1.1f, rb2D.velocity.y) ;
            }
      }

      private void playerTurn(){
            // Switch direction
            FaceRight = !FaceRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
      }
}
