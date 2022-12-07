using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public PauseMenu pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            pauseMenu.turnOnPauseMenu();
            Time.timeScale = 0f;
        }
    }
}
