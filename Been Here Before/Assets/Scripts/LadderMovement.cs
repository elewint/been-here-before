/* Reference Link: https://www.youtube.com/watch?v=yyg0yV2roPk */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
	public Animator anim;
        private float vertical;
        private float speed = 5f;
        private bool isLadder;
        private bool isClimbing;

        [SerializeField] private Rigidbody2D rb;
        private PlayerJump playerJump;

	void Start()
	{
		anim = gameObject.GetComponentInChildren<Animator>();
                playerJump = GetComponent<PlayerJump>();
	}


        void Update()
        {
                vertical = Input.GetAxis("Vertical");
                if (isLadder && Mathf.Abs(vertical) > 0f) {
                        isClimbing = true;
                        playerJump.usingLadder = true;
			anim.SetBool("UsingLadder", true);
                } else {
                        playerJump.usingLadder = false;
			anim.SetBool("UsingLadder", false);
		}
        }

        private void FixedUpdate()
        {
                if (isClimbing) {
                        rb.gravityScale = 0f;
                        rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
                } else {
                        rb.gravityScale = 0.75f;
                }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Ladder")) {
                        isLadder = true;
                        playerJump.usingLadder = true;
                }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
                if (collision.CompareTag("Ladder")) {
                        isLadder = false;
                        isClimbing = false;
                }
        }

}
