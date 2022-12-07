using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    public List<GameObject> paintings;
    public List<GameObject> slots;
    
    private LayerMask paintingMask;
    
    void Start()
    {
        paintingMask = LayerMask.GetMask("Pickup"); /* Layer mask for pick up */
    }
    
    // TODO: NOTE TO FUTURE ELI! disabling the rigidbody simulation stops
    // TODO: collision detection :(
    void Update()
    {
        // foreach (GameObject slot in slots) {
        //         // get 
                
        //     Collider2D painting = Physics2D.OverlapCircle(slot.transform.position, 2f, paintingMask);
            
        //     if (painting)
        //     {
        //         Debug.Log("found painting!");
        //     }
        // }
    }
    
    public void CheckLevel()
    {
        Debug.Log("checking level!");
        // slots have tags corresponding to the paintings that belong there
        // get all collisions between smth in pickup layer and slot layer
        // if all tags are the same, then level is complete!
        foreach (GameObject slot in slots) {
            // get 
            
            Debug.Log(slot.transform.position);
            Collider2D painting = Physics2D.OverlapCircle(slot.transform.position, 2f, paintingMask);
            
            if (painting)
            {
                Debug.Log("found painting!");
            }

            // if (painting) {
            //     Debug.Log("found painting!");
            //     Debug.Log(painting.gameObject);

            //     if (painting.gameObject.tag != slot.tag) {
            //         Debug.Log("fail");
            //         return;
            //     }
            // } else {
            //     return;
            // }
        }
        // if all tags are the same, then level is complete!
        Debug.Log("Level Complete!");
    }
}
