using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public bool beingDropped = false;
    private Collider2D playerCollider;
    
    void Awake()
    {
        playerCollider = transform.parent.gameObject.GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (beingDropped && other.gameObject.layer == LayerMask.NameToLayer("Pickup"))
        {
            
            if (playerCollider)
            {
                Physics2D.IgnoreCollision(other, playerCollider, false);
            }
            
            beingDropped = false;
        }
    }
}
