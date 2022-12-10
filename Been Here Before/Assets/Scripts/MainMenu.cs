using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject creditsButton;
    public GameObject optionsButton;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        creditsMenu.SetActive(false);
        playButton.SetActive(false);
        quitButton.SetActive(false);
        creditsButton.SetActive(false);
    }

    public void ExitOptions()
    {
        optionsMenu.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
        creditsButton.SetActive(true);
    }

    public void CreditsMenu()
    {
        creditsMenu.SetActive(true);
        optionsMenu.SetActive(false);
        playButton.SetActive(false);
        quitButton.SetActive(false);
        optionsButton.SetActive(false);
        creditsButton.SetActive(false);
    }

    public void ExitCredits()
    {
        creditsMenu.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
        optionsButton.SetActive(true);
        creditsButton.SetActive(true);
    }

}
