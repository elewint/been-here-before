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
        LayerMask paintingMask = LayerMask.GetMask("Pickup"); /* Layer mask for pick up */
    }
    
    public void CheckLevel()
    {
        Debug.Log("checking level!");
        // slots have tags corresponding to the paintings that belong there
        // get all collisions between smth in pickup layer and slot layer
        // if all tags are the same, then level is complete!
        foreach (GameObject slot in slots) {
            // get all collisions between smth in pickup layer and slot layer
            Collider2D painting = Physics2D.OverlapCircle(slot.transform.position, 1.5f, paintingMask);

            if (painting) {
                Debug.Log(painting.gameObject);

                if (painting.gameObject.tag != slot.tag) {
                    Debug.Log("fail");
                    return;
                }
            }
        }
        // if all tags are the same, then level is complete!
        Debug.Log("Level Complete!");
    }
}
