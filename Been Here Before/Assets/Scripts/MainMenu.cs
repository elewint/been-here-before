using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public bool showOptions;
    public bool showCredits;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    void Start()
    {
        showOptions = false;
        showCredits = false;
    }

    void Update()
    {
        if (showOptions) {
            optionsMenu.SetActive(true);
        } else {
            optionsMenu.SetActive(false);
        }

        if (showCredits) {
            creditsMenu.SetActive(true);
        } else {
            creditsMenu.SetActive(false);
        }

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
        showOptions = true;
    }

    public void ExitOptions()
    {
        showOptions = false;
    }

    public void CreditsMenu()
    {
        showCredits = true;
    }

    public void ExitCredits()
    {
        showCredits = false;
    }

}
