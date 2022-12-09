using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public bool functionalDoor = true;
    public Sprite doorOpenSprite;
    public Sprite doorClosedSprite;
    public bool doorOpen = false;

    private GameObject door;
    
    void Awake()
    {
        door = transform.GetChild(0).gameObject;
        SetDoorSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        if (functionalDoor && doorOpen && collider.name == "Player")
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OpenDoor()
    {
        doorOpen = true;
        SetDoorSprite();
    }
    
    public void CloseDoor()
    {
        doorOpen = false;
        SetDoorSprite();
    }
    
    private void SetDoorSprite()
    {
        if (doorOpen)
        {
            door.GetComponent<SpriteRenderer>().sprite = doorOpenSprite;
        }
        else
        {
            door.GetComponent<SpriteRenderer>().sprite = doorClosedSprite;
        }
    }
}
