using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FinalReveal : MonoBehaviour
{
    public GameObject face1;
    public GameObject face2;
    public GameObject world;
    public GameObject player;
    private playerMovement playerMovement;
    private TilemapRenderer[] tilemapRenderers;
    
    void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<playerMovement>();
        // Get TilemapRenderers from children of world
        tilemapRenderers = world.GetComponentsInChildren<TilemapRenderer>();
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerMovement.isAlive = false;
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
        // Turn off the tilemap renderers
        foreach (var tilemapRenderer in tilemapRenderers) {
            tilemapRenderer.enabled = false;
        }

        StartCoroutine(ShowSecondFaceAfterSeconds(1.5f));
    }

    IEnumerator ShowSecondFaceAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        face2.SetActive(true);
    }
}
