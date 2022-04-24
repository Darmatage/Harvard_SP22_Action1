using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: public Animator animator;
    private PlayerBars bars;
    public float buttonTime = 0.3f;
    private CapsuleCollider2D capsuleCollider;
    private CircleCollider2D circleCollider;
    public LayerMask enemyLayer;
    public bool FaceRight = true; // Which way is the player facing?
    public float fallMultiplier = 2.5f;
    public float gravity = -9.81f;
    public float gravityScale = 1;
    public float gravityScaleFalling = 10;
    public LayerMask groundLayer;
    private Vector3 hMove;
    public bool isAlive = true; // Will come from bars when needed
    // private bool isGrounded = true;
    private bool isJumping = false;
    public float jumpForce = 20f;
    private float jumpTime = 0;
    public float lowJumpModifier = 2f;
    private PhysicsMaterial2D currentMaterial;
    public PhysicsMaterial2D materialNoSticky;
    public PhysicsMaterial2D materialSticky;
    public Rigidbody2D rig;
    public static float runSpeed = 10f;
    
    public float startSpeed = 10f;
    // TODO: public AudioSource WalkSFX;

    void Start() {
        bars = GameObject.FindWithTag("Player").GetComponent<PlayerBars>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        rig = transform.GetComponent<Rigidbody2D>();

        // SetMaterial(materialSticky);
        currentMaterial = materialNoSticky;
    }

    void FixedUpdate() {
        // Changes velocity on hills and slides
        if (hMove.x == 0) {
            rig.velocity = new Vector2(rig.velocity.x / 1.1f, rig.velocity.y);
        }

        if (bars.brownSticky.curValue > 0 && currentMaterial == materialNoSticky) {
            SetMaterial(materialSticky);
        }
        else if (bars.brownSticky.curValue <= 0 && currentMaterial == materialSticky) {
            SetMaterial(materialNoSticky);
        }
    }

    void Update() {
        if (isAlive) {
            Move();
            Jump();
        }        
    }

    public bool isGrounded() {
        return circleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    public void Jump() {
        // Future idea for "snapping" to ground/platform.
        /*if (groundCheck.isGrounded && velocity < 0) {
            float floorHeight = 0.7f;
            velocity = 0;
            transform.position = new Vector3(transform.position.x, groundCheck.surfacePosition.y + floorHeight, transform.position.z);
        }*/

        if (Input.GetButtonDown("Jump")) {
            if (!isGrounded()) {
                // Only allow extra jumps if player has yellow jelly available
                if (bars.yellowJelly.curValue > 0) {
                    bars.Burn(bars.yellowJelly, 5f);
                }
                else {
                    return;
                }
            }

            isJumping = true;
            jumpTime = 0;

            // animator.SetTrigger("Jump");
            // JumpSFX.Play();
        }

        if (isJumping) {
            // rig.velocity = Vector2.up * jumpForce;
            // rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            jumpTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) | jumpTime > buttonTime) {
            isJumping = false;
        }

        /*
        NOTE:
        There are two jump modes here:
        - Floaty jump: changes the velocity of the rig.
        - Lame jump: changes the gravity scale of the rig.

        Neither work well :/
        */
        if (rig.velocity.y < 0) {
            rig.velocity += Vector2.up * gravity * (fallMultiplier - 1) * Time.deltaTime;
            rig.gravityScale = 10;
        }

        else if (rig.velocity.y >= 0 && !isGrounded()) {
            rig.velocity += Vector2.up * gravity * (lowJumpModifier - 1) * Time.deltaTime;
            rig.gravityScale = 1;
        }
    }

    public void Move() {
        float x = Input.GetAxisRaw("Horizontal");

        if (isJumping) {
            x = x / 2;
        }
 
        rig.velocity = new Vector2(x * runSpeed, rig.velocity.y);

        if ((x < 0 && !FaceRight) || (x > 0 && FaceRight)) {
            PlayerTurn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isJumping = false;
        }
    }

    private void PlayerTurn() {
        // Switch direction
        FaceRight = !FaceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
		Debug.Log("player is facing right" +FaceRight);
    }

    public void SetMaterial(PhysicsMaterial2D m) {
        capsuleCollider.sharedMaterial = m;
    }
}
