using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour {
        //public Animator animator;
        public Rigidbody2D rb2D;
        public static float runSpeed = 10f;
        public float startSpeed = 10f;
        public bool isAlive = true;
       
        public Collider2D DeathCollider;

        private bool FaceRight = false; // determine which way player is facing.
        //public AudioSource WalkSFX;
        // private Vector3 hMove;
        private float moveHorizontal;

        //level 5 death
        //public Collider2D deathCollider;
        //private bool deathColliderActive = true;

        public void Start(){
                //animator = gameObject.GetComponentInChildren<Animator>();
                rb2D = GetComponent<Rigidbody2D>();

                DeathCollider = GetComponent<Collider2D>();
                
        }

        void Update(){
                //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
                // hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
                if (isAlive == true){
                        // transform.position = transform.position + hMove * runSpeed * Time.deltaTime;
                        moveHorizontal = Input.GetAxis("Horizontal");
                        rb2D.velocity = new Vector2(moveHorizontal * runSpeed, rb2D.velocity.y);
                        
                        // Player speeds up
                        if (moveHorizontal != 0){
                                if (runSpeed < 13.5f){
                                        runSpeed = Mathf.Lerp(runSpeed, 13.5f, 0.001f);
                                }
                        } else {
                                runSpeed = startSpeed;
                        }

                        if (Input.GetAxis("Horizontal") != 0){
                        //       animator.SetBool ("Walk", true);
                        //       if (!WalkSFX.isPlaying){
                        //             WalkSFX.Play();
                        //      }
                        } else {
                        //      animator.SetBool ("Walk", false);
                        //      WalkSFX.Stop();
                        }

                        // Turning: Reverse if input is moving the Player right and Player faces left
                        if ((moveHorizontal < 0 && !FaceRight) || (moveHorizontal > 0 && FaceRight)){
                                playerTurn();
                        }
                }

        }

            public void OnTriggerEnter2D(Collider2D dc) {
                //if (dc.GameObject.tag == "Die") {
                     SceneManager.LoadScene(5);
                }
                   
                //}     

        

        void FixedUpdate(){
                //slow down on hills / stops sliding from velocity
                if (moveHorizontal == 0){
                        rb2D.velocity = new Vector2(rb2D.velocity.x / 1.1f, rb2D.velocity.y) ;
                }
        }

        private void playerTurn(){
                // NOTE: Switch player facing label
                FaceRight = !FaceRight;

                // NOTE: Multiply player's x local scale by -1.
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
        }
        
}

