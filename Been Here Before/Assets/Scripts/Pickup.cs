using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform holdSpot;

    private LayerMask pickUpMask;
    private LayerMask slotMask;
    private GameObject itemHolding;
    private LevelChecker lvlChecker;
    private Collider2D playerCollider;

    // Need to know which direction the player is facing – get from movement scipt
    public Vector3 Direction {get; set; }

    void Start()
    {
        // Layer masks for pickup and slot
        pickUpMask = LayerMask.GetMask("Pickup");
        slotMask = LayerMask.GetMask("Slot");
        lvlChecker = GameObject.Find("LevelChecker").GetComponent<LevelChecker>();
        playerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pickup"))
        {
                if (itemHolding)
                {
                    PutDown();

                }
                else
                {
                    PickUp();
                }
        }
    }
    
    void PickUp()
    {
        // pickUpMask will only allow player to get item that can be picked up
        Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, 1.5f, pickUpMask);

        if (pickUpItem)
        {
            itemHolding = pickUpItem.gameObject;

            if (itemHolding && itemHolding.GetComponent<Rigidbody2D>())
            {
                // Stop the body from colliding with other objects
                itemHolding.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                itemHolding.GetComponent<Collider2D>().isTrigger = true;
                
                // Set the position of the item to the holdSpot
                itemHolding.transform.position = holdSpot.position;
                itemHolding.transform.parent = transform;

                // Change the sorting order so that the item is on top of the player
                itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 80;
            }
        }
    }
    
    void PutDown()
    {
        Collider2D slot = Physics2D.OverlapCircle(transform.position + Direction, 1.5f, slotMask);
        
        // Successfully put on slot
        if (slot)
        {
            itemHolding.transform.position = slot.transform.position;
            // Set sorting order to behind player
            // itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 99;
        
            lvlChecker.CheckLevel();
        }
        else
        {
            Physics2D.IgnoreCollision(itemHolding.GetComponent<Collider2D>(), playerCollider, true);

            itemHolding.GetComponent<Collider2D>().isTrigger = false;
            itemHolding.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            // itemHolding.transform.position = transform.position + Direction;     /* Change the positon of the item so that when the item is drop, it dropped right infront of the player */
            if (itemHolding.GetComponent<Drop>())
            {
                itemHolding.GetComponent<Drop>().beingDropped = true;
            }

        }

        // Get the item off the player head and set it to null since the item is no longer being held
        itemHolding.transform.parent = null;
        itemHolding = null;
    }
}
