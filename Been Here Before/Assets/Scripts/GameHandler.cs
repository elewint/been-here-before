using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class GameHandler : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public Volume postProcessingVolume; 
    public CinemachineVirtualCamera worldCam;
    public CinemachineVirtualCamera flashbackCam;
    public GameObject flashbackObject;
    public bool inFlashback = false;
    
    private GameObject sourceDoor;

    void Start()
    {
        // Turn on film grain
        postProcessingVolume.profile.TryGet(out FilmGrain filmGrain);
        filmGrain.active = true;

        // Set saturation to -100
        postProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.saturation.value = -50f;
        
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
        
        if (flashbackObject)
        {
            flashbackObject.SetActive(true);
            // Start dialogue after 1 second delay
            StartCoroutine(StartDialogueAfterSeconds(2f));
        }
    }
    
    public void endFlashBack()
    {
        if (!inFlashback) return;

        // Set priority of cinemachine virtual camera to 20
        flashbackCam.Priority = 10;
        colorWorld();

        sourceDoor.GetComponent<NextSceneTransition>().PublicLoadAfterSeconds(6f);
        
        flashbackObject.SetActive(false);
        inFlashback = false;
    }

    private IEnumerator StartDialogueAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        flashbackObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
