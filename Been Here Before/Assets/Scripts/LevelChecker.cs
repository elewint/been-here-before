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
        foreach (GameObject slot in slots)
        {
            Collider2D painting = Physics2D.OverlapCircle(slot.transform.position, 2f, paintingMask);
            
            if (painting)
            {
                if (painting.gameObject.tag != slot.tag)
                {
                    Debug.Log("Incorrect placement");
                    return;
                }
            }
            else
            {
                return;
            }
        }
        LevelComplete();
    }
    
    void LevelComplete()
    {
        Debug.Log("Level Complete!");

        foreach (GameObject door in doorsToOpen)
        {
            door.GetComponent<NextSceneTransition>().OpenDoor();
        }
    }
}
