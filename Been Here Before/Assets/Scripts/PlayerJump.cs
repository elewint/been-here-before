using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {
    public Animator anim;
    public Rigidbody2D rb;
    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public bool canJump = false;
    public bool isAlive = true;
    public AudioSource JumpSFX;
    public float lowJumpMultiplier = 3f;
    public float fallMultiplier = 3.5f;
    public bool usingLadder = false;

    private float lastJumpTime;

    void Start(){
        anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lastJumpTime = Time.time;
    }

    void Update() {
        if (isAlive && IsGrounded() && Input.GetButtonDown("Jump")) {
            Jump();
        }

        if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump")){
            // Debug.Log("low jump");
            rb.velocity += Vector2.up * Physics.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }

        else if (rb.velocity.y < 0) {
            // Debug.Log("fall");
            rb.velocity += Vector2.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }

    public void Jump() {
        // Measure time since we last jumped. If less than 0.2 seconds, we
        // can't jump again :(
        if (usingLadder || Time.time - lastJumpTime < 0.2f) {
            return;
        }
        
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // Debug.Log("jumping!");
        // anim.SetTrigger("Jump");
        lastJumpTime = Time.time;
        JumpSFX.Play();

        //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, .1f, groundLayer);
        Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, .1f, enemyLayer);
        if ((groundCheck != null) || (enemyCheck != null)) {
            // Debug.Log("I am trouching ground!");
            return true;
        }

        return false;
    }
}