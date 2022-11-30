using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTransition : MonoBehaviour
{

        public string sceneToLoad;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                GameObject player = collision.gameObject;
                if(player.name == "Key") {
                        LoadScene();
                }

        }

        void LoadScene()
        {
                SceneManager.LoadScene(sceneToLoad);
        }

}
