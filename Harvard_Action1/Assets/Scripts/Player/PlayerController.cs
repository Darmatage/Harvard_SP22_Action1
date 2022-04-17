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
    public Transform feet;
    public LayerMask groundLayer;
    public bool isAlive = true; // Will come from bars when needed
    private bool isGrounded = false;
    public float jumpForce = 20f;
    private bool jumping = false;
    private float jumpTime = 0;
    public Rigidbody2D rig;
    public static float runSpeed = 10f;
    
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

            if ((hMove.x < 0 && !FaceRight) || (hMove.x > 0 && FaceRight)) {
                PlayerTurn();
            }

            if (Input.GetButtonDown("Jump") && IsGrounded()) {
                Jump();
                // animator.SetTrigger("Jump");
                // JumpSFX.Play();
            }

            if (jumping) {
                // rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            }

            if (Input.GetKeyUp(KeyCode.Space) | jumpTime > buttonTime) {
                jumping = false;
            }
        }        
    }

    public bool IsGrounded() {
        Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 2f, enemyLayer);
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayer);

        if (groundCheck != null || enemyCheck != null) {
            return true;
        }

        return false;
    }

    public void Jump() {
        jumping = true;
        jumpTime = 0;

        // rig.velocity = Vector2.up * jumpForce;
        
        //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;
    }

    public void Move() {
        hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        transform.position = transform.position + hMove * runSpeed * Time.deltaTime;
    }

    private void PlayerTurn() {
        // Switch direction
        FaceRight = !FaceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
