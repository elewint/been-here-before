using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            Debug.Log("Pause");
            pauseMenu.turnOnPauseMenu();
            Time.timeScale = 0f;
        }
    }
}
