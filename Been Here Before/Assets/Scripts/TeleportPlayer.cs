using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

        private GameObject currentTeleporter;

        void Update()
        {
                if(Input.GetButtonDown("Pickup")) {
                        if(currentTeleporter != null) {
                                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                        }
                }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if(collision.CompareTag("Teleporter")) {
                        currentTeleporter = collision.gameObject;
                }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
                if(collision.CompareTag("Teleporter")) {
                        if(collision.gameObject == currentTeleporter) {
                                currentTeleporter = null;
                        }
                }
        }

}
