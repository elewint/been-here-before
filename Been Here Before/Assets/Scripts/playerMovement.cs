using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
        public float speed = 6f;        /* player movement speed */
        public float jumpSpeed = 8f;    /* player jump speed */
        private Rigidbody2D player;     /* player character */
        private float direction = 0f;   /* player's direction */

        /* Start is called before the first frame update */
        void Start()
        {
                if (gameObject.GetComponent<Rigidbody2D>() != null) {
                        player = GetComponent<Rigidbody2D>();
                }
        }

        /* Update is called once per frame */
        void Update()
        {

                /* Get player's horizontal direction */
                direction = Input.GetAxisRaw("Horizontal");

                /* Move player left or right or stop */
                if (direction > 0f || direction < 0f) {
                        player.velocity = new Vector2(direction * speed, player.velocity.y);
                } else {
                        player.velocity = new Vector2(0, player.velocity.y);
                }

                /* Make player's jump on 'space' */
                if (Input.GetButtonDown("Jump")) {
                        player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                }

        }
}

