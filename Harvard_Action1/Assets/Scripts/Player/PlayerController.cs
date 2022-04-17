using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: public Animator animator;
    private PlayerBars bars;
    public float buttonTime = 0.3f;
    public LayerMask enemyLayer;
    private bool FaceRight = true; // Which way is the player facing?
    public float gravity = -9.81f;
    public float gravityScale = 5;
    public LayerMask groundLayer;
    public bool isAlive = true; // Will come from bars when needed
    private bool isGrounded = true;
    public float jumpForce = 20f;
    private bool jumping = false;
    private float jumpTime = 0;
    public Rigidbody2D rig;
    public static float runSpeed = 10f;
    private float velocity;
    
    public float startSpeed = 10f;
    // TODO: public AudioSource WalkSFX;
    private Vector3 hMove;

    void Start() {
        bars = GameObject.FindWithTag("Player").GetComponent<PlayerBars>();
        rig = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        // Changes velocity on hills and slides
        if (hMove.x == 0) {
            rig.velocity = new Vector2(rig.velocity.x / 1.1f, rig.velocity.y);
        }
    }

    void Update() {
        if (isAlive) {
            Move();
            Jump();
        }        
    }

    

    public void Jump() {
        velocity += gravity * gravityScale * Time.deltaTime;

        /*if (groundCheck.isGrounded && velocity < 0) {
            float floorHeight = 0.7f;
            velocity = 0;
            transform.position = new Vector3(transform.position.x, groundCheck.surfacePosition.y + floorHeight, transform.position.z);
        }*/

        if (Input.GetButtonDown("Jump")) {
            if (!isGrounded) {
                // Only allow extra jumps if player has yellow jelly available
                if (bars.yellowJelly.curValue > 0) {
                    bars.Burn(bars.yellowJelly, 5f);
                }
                else {
                    return;
                }
            }

            isGrounded = false;
            jumping = true;
            jumpTime = 0;
            velocity = jumpForce;

            // animator.SetTrigger("Jump");
            // JumpSFX.Play();
        }

        if (jumping) {
            // rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            // rig.velocity = velocity;
            jumpTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) | jumpTime > buttonTime) {
            jumping = false;
        }
    }

    public void Move() {
        hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        transform.position = transform.position + hMove * runSpeed * Time.deltaTime;

        if ((hMove.x < 0 && !FaceRight) || (hMove.x > 0 && FaceRight)) {
            PlayerTurn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void PlayerTurn() {
        // Switch direction
        FaceRight = !FaceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
