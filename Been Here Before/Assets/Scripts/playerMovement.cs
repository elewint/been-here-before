using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
        public float speed = 6f;        /* player movement speed */
        public float jumpSpeed = 8f;    /* player jump speed */
        private Rigidbody2D player;     /* player character */
        private float direction = 0f;   /* player's direction */
        private SpriteRenderer sprite;  /* player's sprite */

        /* Start is called before the first frame update */
        void Start()
        {
                if (gameObject.GetComponent<Rigidbody2D>() != null)
                {
                        player = GetComponent<Rigidbody2D>();
                }
                
                sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        /* Update is called once per frame */
        void Update()
        {
                if (Time.timeScale == 0) return;

                /* Get player's horizontal direction */
                direction = Input.GetAxisRaw("Horizontal");

                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                        Cursor.visible = true;
                } else if (direction != 0)
                {
                        Cursor.visible = false;
                }

                /* Move player left or right or stop */
                if (direction > 0f)
                {
                        sprite.flipX = false;
                }
                else if (direction < 0f)
                {
                        sprite.flipX = true;
                }

                player.velocity = new Vector2(direction * speed, player.velocity.y);

                /* Make player's jump on 'space' */
                if (Input.GetButtonDown("Jump") && Mathf.Abs(player.velocity.y) < 0.02f)
                {
                        player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                }

        }
}

