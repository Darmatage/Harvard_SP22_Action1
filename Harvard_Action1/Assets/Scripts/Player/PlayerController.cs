using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
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
    public PhysicsMaterial2D materialBouncy;
    public PhysicsMaterial2D materialBouncySticky;
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
		animator= GetComponentInChildren<Animator>();
    }

    void Start() {
		//audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();

        // SetMaterial(materialSticky);
        currentMaterial = materialNoSticky;
    }

    void FixedUpdate() {
        // Changes velocity on hills and slides
        if (hMove.x == 0) {
            rig.velocity = new Vector2(rig.velocity.x / 1.1f, rig.velocity.y);
        }
    }

    void SetMaterialState() {
        if (bars.whiteCloud.GetValue() > 0 && bars.brownSticky.GetValue() > 0 && currentMaterial != materialBouncySticky) {
            SetMaterial(materialBouncySticky);
        }
        else if (bars.whiteCloud.GetValue() > 0 && currentMaterial != materialBouncy) {
            SetMaterial(materialBouncy);
        }
        else if (bars.brownSticky.GetValue() > 0 && currentMaterial != materialSticky) {
            SetMaterial(materialSticky);
        }
        else if (bars.brownSticky.GetValue() <= 0 && currentMaterial != materialNoSticky) {
            SetMaterial(materialNoSticky);
        }
    }

    void Update() {
        if (isAlive) {
            SetMaterialState();
			Move();		

            if (Input.GetButtonDown("Jump")) {
				
                 //Debug.Log("Jump: Button pushed");
                // Check for extra jumps
                if (!isGrounded()) {
                    // Debug.Log("Jump: Second Jump attempted");
                    // Only allow extra jumps if player has yellow jelly available
                    if (bars.yellowJelly.GetValue() > 1f) {
                        bars.Burn(bars.yellowJelly, 2f);
                        // Debug.Log("Jump: Second Jump allowed");
                        Jump(); // Do an extra jump
						
                    }
                    else {
												
                        // Debug.LogFormat("Jump: Second Jump declined: {0} {1}", bars.yellowJelly.startValue, bars.yellowJelly.curValue);
                    }
                }
				
                // Player is grounded
				
                else {
                    // Debug.Log("Jump: First Jump");
					Jump();
                }
            }

			animator.SetBool ("Jump", false);

            if (rig.velocity.y < 0) {
                rig.velocity += Vector2.up * gravity * fallGravityMultiplier * Time.deltaTime;
				
                //rig.gravityScale = 10;
            }

            else if (rig.velocity.y >= 0 && !isGrounded()) {
                rig.velocity += Vector2.up * gravity * jumpGravityMultiplier * Time.deltaTime;
				
                //rig.gravityScale = 1;
            }
        }

        if (isJumping==true) {
            animator.SetBool ("Jump", true);
        }
        else {
            animator.SetBool ("Jump", false);
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
		animator.SetBool("Jump", true);
        JumpSFX.Play();
        // rig.velocity = new Vector2(rig.velocity.x, jumpForce);
        rig.velocity = Vector2.up * jumpForce;
    }

    public void Move() {
        float x = Input.GetAxisRaw("Horizontal");
        if (isJumping) 
		{			
            x = x / 2;			
        }
		
 
        rig.velocity = new Vector2(x * runSpeed, rig.velocity.y);

        if (x!=0) {
            animator.SetBool("Walk", true);
        } else
        {
            animator.SetBool("Walk", false);
        }
		
        if ((x < 0 && !FaceRight) || (x > 0 && FaceRight)) {
            PlayerTurn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isJumping = false;
			animator.SetBool("Jump",false);
			
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
