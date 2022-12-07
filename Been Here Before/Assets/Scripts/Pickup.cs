using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
 public Transform holdSpot;  /* Location where penguin will be hold */
        public GameObject successfulMessage;
        public GameObject unsuccessfulMessage;
        public GameObject door;

        private LayerMask pickUpMask;
        private LayerMask slotMask;
        private GameObject itemHolding;     /* Having private means when we have it pick up, we store it in this variable. When we want to drop it, we can just access this variable again */
        private LevelChecker lvlChecker;

        public Vector3 Direction {get; set; }  /* Pick Up Logic: Need to know which direction the nurse penguin is facing. Get from movement scipt */

        void Start()
        {
                pickUpMask = LayerMask.GetMask("Pickup"); /* Layer mask for pick up */
                slotMask = LayerMask.GetMask("Slot"); /* Layer mask for slot */
                lvlChecker = GameObject.Find("LevelChecker").GetComponent<LevelChecker>();
        }

        // slots have tags corresponding to the paintings that belong there
        // get all collisions between smth in pickup layer and slot layer
        // if all tags are the same, then level is complete!

        void Update()
        {

                if (Input.GetButtonDown("Pickup")) {  /* Check for key input */

                                if (itemHolding) {  /* If player is currently holding item, drop the item */

                                        Collider2D slot = Physics2D.OverlapCircle(transform.position + Direction, 1.5f, slotMask);
                                        
                                        if (slot) {
                                                // Successfully put on slot
                                                lvlChecker.CheckLevel();
                                                // successfulMessage.SetActive(true);
                                                // unsuccessfulMessage.SetActive(false);
                                                // door.SetActive(true);
                                                itemHolding.transform.position = slot.transform.position;
                                                itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 99; /* Set sorting order to behind player */

                                        } else {
                                                itemHolding.transform.position = transform.position + Direction;     /* Change the positon of the item so that when the item is drop, it dropped right infront of the player */

                                                if (itemHolding.GetComponent<Rigidbody2D>()) {
                                                        itemHolding.GetComponent<Rigidbody2D>().simulated = true;  /* Check to make sure that the object stop following the player */
                                                }
                                        }

                                        itemHolding.transform.parent = null;   /* Get the item off the player head */
                                        itemHolding = null; /* Set to null because item is no longer being held */

                                } else {    /* If player is not currently holding an item, pick the item up */
                                        Debug.Log("should pick up");

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
