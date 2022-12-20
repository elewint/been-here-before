using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public bool beingDropped = false;
    public List<Collider2D> ignoredColliders;

    private Collider2D playerCollider;
    
    void Awake()
    {
        ignoredColliders = new List<Collider2D>();
        playerCollider = transform.parent.gameObject.GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (beingDropped && ignoredColliders.Contains(other)) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Pickup"))
            {
                
                if (playerCollider)
                {
                    Physics2D.IgnoreCollision(other, playerCollider, false);
                    ignoredColliders.Remove(other);
                    Debug.Log("allowing collisions with " + other.name);
                }
                
                beingDropped = false;
            }
        }
    }
}
