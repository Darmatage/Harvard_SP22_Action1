using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: public Animator animator;
    private PlayerBars bars;
    private CapsuleCollider2D capsuleCollider;
    private CircleCollider2D circleCollider;
    public LayerMask enemyLayer;
	public LayerMask breakableWall;
    public bool FaceRight = true; // Which way is the player facing?
    public float fallGravityMultiplier = 5f;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    private Vector3 hMove;
    public bool isAlive = true; // Will come from bars when needed
    // private bool isGrounded = true;
    private bool isJumping = false;
    public float jumpForce = 20f;
    public float jumpGravityMultiplier = 5f;
    private PhysicsMaterial2D currentMaterial;
    public PhysicsMaterial2D materialNoSticky;
    public PhysicsMaterial2D materialSticky;
    public Rigidbody2D rig;
    public static float runSpeed = 10f;
    //AudioManager audioManager;
	public AudioSource JumpSFX;
    public float startSpeed = 10f;
    // TODO: public AudioSource WalkSFX;

    void Awake() {
        bars = GetComponent<PlayerBars>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        rig = transform.GetComponent<Rigidbody2D>();
    }

    void Start() {
        Debug.LogFormat("Sticky Start value: {0} {1}", bars.brownSticky.startValue, bars.brownSticky.curValue);
        Debug.LogFormat("Health Start value: {0} {1}", bars.health.startValue, bars.brownSticky.curValue);
        Debug.LogFormat("Yellow Start value: {0} {1}", bars.yellowJelly.startValue, bars.brownSticky.curValue);
		//audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();

        // SetMaterial(materialSticky);
        currentMaterial = materialNoSticky;

        InvokeRepeating("OutputTime", 1f, 1f);
    }

    void OutputTime() {
        Debug.LogFormat("Sticky FU value: {0} {1}", bars.brownSticky.startValue, bars.brownSticky.curValue);
        Debug.LogFormat("Health FU value: {0} {1}", bars.health.startValue, bars.brownSticky.curValue);
        Debug.LogFormat("Yellow FU value: {0} {1}", bars.yellowJelly.startValue, bars.brownSticky.curValue);
    }

    void FixedUpdate() {
        // Changes velocity on hills and slides
        if (hMove.x == 0) {
            rig.velocity = new Vector2(rig.velocity.x / 1.1f, rig.velocity.y);
        }
    }

    void SetMaterialState() {
        if (bars.brownSticky.GetValue() > 0 && currentMaterial == materialNoSticky) {
            SetMaterial(materialSticky);
        }
        else if (bars.brownSticky.GetValue() <= 0 && currentMaterial == materialSticky) {
            SetMaterial(materialNoSticky);
        }
    }

    void Update() {
        if (isAlive) {
            SetMaterialState();
            Move();

            if (Input.GetButtonDown("Jump")) {
                Debug.Log("Jump: Button pushed");
                // Check for extra jumps
                if (!isGrounded()) {
                    Debug.Log("Jump: Second Jump attempted");
                    // Only allow extra jumps if player has yellow jelly available
                    if (bars.yellowJelly.GetValue() > 0) {
                        bars.Burn(bars.yellowJelly, 2f);
                        Debug.Log("Jump: Second Jump allowed");
                        Jump(); // Do an extra jump
                    }
                    else {
                        Debug.LogFormat("Jump: Second Jump declined: {0} {1}", bars.yellowJelly.startValue, bars.yellowJelly.curValue);
                    }
                }
                // Player is grounded
                else {
                    Debug.Log("Jump: First Jump");
                    Jump();
                }
            }

            if (rig.velocity.y < 0) {
                rig.velocity += Vector2.up * gravity * fallGravityMultiplier * Time.deltaTime;
                //rig.gravityScale = 10;
            }

            else if (rig.velocity.y >= 0 && !isGrounded()) {
                rig.velocity += Vector2.up * gravity * jumpGravityMultiplier * Time.deltaTime;
                //rig.gravityScale = 1;
            }
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

        isJumping = true;
        JumpSFX.Play();
        // rig.velocity = new Vector2(rig.velocity.x, jumpForce);
        rig.velocity = Vector2.up * jumpForce;
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
		// Debug.Log("player is facing left: " +FaceRight);
    }

    public void SetMaterial(PhysicsMaterial2D m) {
        capsuleCollider.sharedMaterial = m;
    }
}
