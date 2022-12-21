using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Logic:
// 1. Slots have tags corresponding to the paintings that belong there
// 2. When painting is dropped in slot, check if tags are the same
// 3. If all tags match, then level is complete!

public class LevelChecker : MonoBehaviour
{
    public List<GameObject> paintings;
    public List<GameObject> slots;
    public List<GameObject> doorsToOpen;
    
    private LayerMask paintingMask;
    
    void Start()
    {
        // Layer masks for paintings, which are on Pickup layer
        paintingMask = LayerMask.GetMask("Pickup");
    }
    
    public void CheckLevel()
    {
        // List will store all the paintings so they can be locked
        // in place if the level is complete
        List<Collider2D> paintings = new List<Collider2D>();

        foreach (GameObject slot in slots)
        {
            Collider2D painting = Physics2D.OverlapCircle(slot.transform.position, 2f, paintingMask);
            
            if (painting)
            {
                paintings.Add(painting);

                if (painting.gameObject.tag != slot.tag)
                {
                    // Debug.Log("Incorrect placement");
                    return;
                }
            }
            else
            {
                return;
            }
        }
        
        // If all paintings are in the correct slots, lock them in place
        foreach (Collider2D painting in paintings)
        {
            painting.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        LevelComplete();
    }
    
    void LevelComplete()
    {
        // Debug.Log("Level Complete!");

        foreach (GameObject door in doorsToOpen)
        {
            door.GetComponent<NextSceneTransition>().OpenDoor();
        }
    }
}
