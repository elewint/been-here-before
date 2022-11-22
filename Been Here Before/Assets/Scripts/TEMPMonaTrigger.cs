using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPMonaTrigger : MonoBehaviour
{
    public DialogueTrigger dt;

    private void OnTriggerEnter2D(Collider2D collision)  
        {
            GameObject player = collision.gameObject;
            if(player.name == "Player") {
               dt.TriggerDialogue(); 
            }
        }
}
