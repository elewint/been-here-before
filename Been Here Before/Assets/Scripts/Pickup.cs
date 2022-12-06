using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
 public Transform holdSpot;  /* Location where penguin will be hold */
        public LayerMask pickUpMask;
        public LayerMask slotMask;

        private GameObject itemHolding;     /* Having private means when we have it pick up, we store it in this variable. When we want to drop it, we can just access this variable again */

        public Vector3 Direction {get; set; }  /* Pick Up Logic: Need to know which direction the nurse penguin is facing. Get from movement scipt */

        void Start()
        {
                pickUpMask = LayerMask.GetMask("Pickup"); /* Layer mask for pick up */
                slotMask = LayerMask.GetMask("Slot"); /* Layer mask for slot */
        }

        void Update()
        {

                if (Input.GetButtonDown("Pickup")) {  /* Check for key input */

                                if (itemHolding) {  /* If player is currently holding item, drop the item */

                                        Collider2D slot = Physics2D.OverlapCircle(transform.position + Direction, 1.5f, slotMask);
                                        
                                        if (slot) {
                                                itemHolding.transform.position = slot.transform.position;
                                                itemHolding.transform.parent = null;
                                                itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 99; /* Set sorting order to behind player */
                                                itemHolding = null; /* Set to null because item is no longer being held */

                                        } else {
                                                itemHolding.transform.position = transform.position + Direction;     /* Change the positon of the item so that when the item is drop, it dropped right infront of the player */
                                                itemHolding.transform.parent = null;   /* Get the item off the player head */

                                                if (itemHolding.GetComponent<Rigidbody2D>()) {
                                                        itemHolding.GetComponent<Rigidbody2D>().simulated = true;  /* Check to make sure that the object stop following the player */
                                                }

                                                itemHolding = null; /* Set to null because item is no longer being held */
                                        }

                                

                                } else {    /* If player is not currently holding an item, pick the item up */

                                        Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, 1.5f, pickUpMask);   /* pickUpMask will only allow player to get item that can be picked up */

                                        if (pickUpItem) {   /* If there's an item that can be picked up */
                                                itemHolding = pickUpItem.gameObject;    /* store game object into itemHolding */
                                                itemHolding.transform.position = holdSpot.position;     /* Change the positon of the item to the holding spot position */
                                                itemHolding.transform.parent = transform;   /* Set the parent of the item to the player so that the item follows the player */
                                                itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 101; /* Change the sorting order so that the item is on top of the player */
                                        }

                                        if (itemHolding && itemHolding.GetComponent<Rigidbody2D>()) {
                                                itemHolding.GetComponent<Rigidbody2D>().simulated = false;  /* Check to make sure that the object follows the player */
                                        }

                                }

                }
                
        }
        
}
