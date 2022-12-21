using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public Volume postProcessingVolume; 
    public CinemachineVirtualCamera worldCam;
    public CinemachineVirtualCamera flashbackCam;
    public GameObject flashbackObject;
    public bool inFlashback = false;
    public bool finalFlashback = false;
    public bool finalDialogueSentence = false;
    public GameObject finalDialogueObject;
    public GameObject lever;
    public GameObject leverUnpulled;
    public GameObject leverPulled;
    
    private GameObject sourceDoor;

    void Start()
    {
        // Turn on film grain
        postProcessingVolume.profile.TryGet(out FilmGrain filmGrain);
        filmGrain.active = true;

        // Set saturation to -100
        postProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.saturation.value = -75f;
        
        // Set vignette intensity to 0
        postProcessingVolume.profile.TryGet(out Vignette vignette);
        vignette.intensity.value = 0f;
        
        // Enable chromatic aberration
        postProcessingVolume.profile.TryGet(out ChromaticAberration chromaticAberration);
        chromaticAberration.active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            pauseMenu.turnOnPauseMenu();
            Time.timeScale = 0f;
        }
    }
    
    public void colorWorld()
    {
        // Set priority of cinemachine virtual camera to 20
        worldCam.Priority = 20;
        
        // Turn up the saturation to 0
        postProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.saturation.value = 0f;
        
        // Disable chromatic aberration
        postProcessingVolume.profile.TryGet(out ChromaticAberration chromaticAberration);
        chromaticAberration.active = false;
    }
    
    public void FlashBack(GameObject source)
    {
        inFlashback = true;
        sourceDoor = source;
        // Set priority of cinemachine virtual camera to 20
        flashbackCam.Priority = 30;
        
        // Bring the vignette intensity up to 0.5 over 2 seconds
        StartCoroutine(FlashbackVignetteFromTo(0f, 0.5f, 2f));
        
        // Fade the player's alpha over 2 seconds
        StartCoroutine(FlashbackFadeAlphaFromTo(1f, 0f, 2f));

        // Disable player movement and jump
        GameObject player = GameObject.Find("Player");
        player.GetComponent<playerMovement>().isAlive = false;
        player.GetComponent<PlayerJump>().isAlive = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        if (flashbackObject)
        {
            flashbackObject.SetActive(true);
            if (lever) lever.SetActive(false);
            // Start dialogue after 1 second delay
            StartCoroutine(StartDialogueAfterSeconds(2f));
        }
    }
    
    public void endFlashBack()
    {
        if (!inFlashback) return;
        if (finalFlashback) 
        {
            StartCoroutine(FinalFlashAfterDelay(3.5f));
            return;
        }

        // Set priority of cinemachine virtual camera to 20
        flashbackCam.Priority = 10;
        colorWorld();

        sourceDoor.GetComponent<NextSceneTransition>().PublicLoadAfterSeconds(6f);
        
        flashbackObject.SetActive(false);
        inFlashback = false;
    }
    
    private void FinalFlashback()
    {
        // Flash white by setting post exposure to 100
        GameObject whiteFlash = GameObject.Find("WhiteFlash");
        // Set alpha of white flash image to 100
        whiteFlash.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        // Disable first lines of dialogue
        flashbackObject.SetActive(false);

        finalDialogueSentence = true;
        StartCoroutine(StartDialogueAfterSeconds(3f));
    }

    public void endFinalFlashback()
    {
        sourceDoor.GetComponent<NextSceneTransition>().PublicLoadAfterSeconds(0.5f);
        
        inFlashback = false;
    }

    private IEnumerator StartDialogueAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        if (finalDialogueSentence) {
            finalDialogueObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            yield break;
        } else {
            flashbackObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private IEnumerator FinalFlashAfterDelay(float seconds)
    {
        leverUnpulled.SetActive(false);
        leverPulled.SetActive(true);
        
        // Play machine activating sound effect
        finalDialogueObject.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(seconds);
        FinalFlashback();
    }

    private IEnumerator FlashbackVignetteFromTo(float from, float to, float time)
    {
        postProcessingVolume.profile.TryGet(out Vignette vignette);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            vignette.intensity.value = Mathf.Lerp(from, to, t);
            yield return null;
        }
    }

    private IEnumerator FlashbackFadeAlphaFromTo(float from, float to, float time)
    {
        GameObject player = GameObject.Find("player_art");
        SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        Color playerColor = playerSpriteRenderer.color;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            playerColor.a = Mathf.Lerp(from, to, t);
            playerSpriteRenderer.color = playerColor;
            yield return null;
        }
    }
}
