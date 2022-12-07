using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public bool beingDropped = false;

    void OnTriggerExit2D(Collider2D other)
    {
        if (beingDropped && other.gameObject.tag == "Player")
        {
            Debug.Log("dropped");
            // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, false);
        }
    }
}
