using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {
    //public Animator anim;
    public Rigidbody2D rb;
    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public bool canJump = false;
    public bool isAlive = true;
    //public AudioSource JumpSFX;
    public float lowJumpMultiplier = 3f;
    public float fallMultiplier = 3.5f;


    void Start(){
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if ((Input.GetButtonDown("Jump")) && (IsGrounded()) && (isAlive == true)) {
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
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // anim.SetTrigger("Jump");
        // JumpSFX.Play();

        //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;
    }

    public bool IsGrounded() {
            Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, .2f, groundLayer);
            Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, .2f, enemyLayer);
            if ((groundCheck != null) || (enemyCheck != null)) {
                // Debug.Log("I am trouching ground!");
                return true;
            }
            return false;
    }
}