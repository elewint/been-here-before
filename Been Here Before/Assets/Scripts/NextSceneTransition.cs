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
    public bool triggerFlashback = false;
    
    private GameHandler gameHandler;

    private GameObject door;
    
    void Awake()
    {
        door = transform.GetChild(0).gameObject;
        SetDoorSprite();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        if (functionalDoor && doorOpen && collider.name == "Player")
        {
            // Door can only be entered once
            functionalDoor = false;

            // If there's a flashback, wait 5 seconds before triggering it
            // then wait for user to complete dialogue before loading scene
            if (triggerFlashback) StartCoroutine(FlashbackAfterSeconds(1f));
            // Wait 6 seconds before loading scene
            else {
                gameHandler.colorWorld();
                StartCoroutine(LoadAfterSeconds(6f));
            }
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

    public void LoadAfterFlashback()
    {
        LoadScene();
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
    
    public void PublicLoadAfterSeconds(float time)
    {
        StartCoroutine(LoadAfterSeconds(time));
    }

    IEnumerator LoadAfterSeconds(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        LoadScene();
    }

    IEnumerator FlashbackAfterSeconds(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameHandler.FlashBack(gameObject);
    }
}
