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
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    
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
        // Pause player movement for 3 seconds and slowly zoom out main camera
        // Time.timeScale = 0f;
        // Set priority of cinemachine virtual camera to 20
        cinemachineVirtualCamera.Priority = 20;
        
        // Turn up the saturation to 0
        postProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.saturation.value = 0f;
        
        // Disable chromatic aberration
        postProcessingVolume.profile.TryGet(out ChromaticAberration chromaticAberration);
        chromaticAberration.active = false;
    }
}
