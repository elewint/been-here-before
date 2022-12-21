using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalReveal : MonoBehaviour
{
    public GameObject face1;
    public GameObject face2;
    public GameObject world;
    public GameObject player;
    private playerMovement playerMovement;
    private SpriteRenderer blackScreen;
    
    void Awake()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<playerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerMovement.isAlive = false;
            player.GetComponent<PlayerJump>().isAlive = false;

            ShowFaces();
        }
    }

    void ShowFaces()
    {
        face1.SetActive(true);
        // Turn off object's sprite renderer
        GetComponent<SpriteRenderer>().enabled = false;
        // Disable player velocity
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        
        // Fade alpha of black screen from 0 to 1 over 2 seconds
        GameObject blackScreenObject = GameObject.Find("BlackScreen");
        if (blackScreenObject) blackScreen = blackScreenObject.GetComponent<SpriteRenderer>();

        if (blackScreen) StartCoroutine(FadeBlackScreenIn(0f, 1f, 2f));

        StartCoroutine(ShowSecondFaceAfterSeconds(3.5f));
    }

    IEnumerator ShowSecondFaceAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        face2.SetActive(true);
    }

    IEnumerator FadeBlackScreenIn(float start, float end, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            blackScreen.color = new Color(0f, 0f, 0f, Mathf.Lerp(start, end, i));
            yield return null;
        }
    }
}
