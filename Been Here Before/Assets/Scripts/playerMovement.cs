using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
        public float speed = 6f; /* player movement speed */
        public AudioSource moveSFX; /* walking sfx */
        private Vector3 change; /* player movement direction */
        private Rigidbody2D rb2d;

        /* Start is called before the first frame update */
        void Start()
        {
                if (gameObject.GetComponent<Rigidbody2D>() != null) {
                        rb2d = GetComponent<Rigidbody2D>();
                }
        }

        /* Update is called once per frame */
        void FixedUpdate()
        {
                change = Vector3.zero;
                change.x = Input.GetAxisRaw("Horizontal");      /* change horizontal input */
                change.y = Input.GetAxisRaw("Vertical");        /* change vertical input */
                UpdateAnimationAndMove();                       /* update animation */
        }

        /* Animation controller */
        void UpdateAnimationAndMove()
        {
                if (change != Vector3.zero) {   /* if player is moving, activate the animation */
                        rb2d.MovePosition(transform.position + change * speed * Time.deltaTime);
                        if (moveSFX.isPlaying == false){
                                moveSFX.Play();
                        }
                }
        }
}

