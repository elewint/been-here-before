using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
        public GameObject player;

        private Vector3 offset;

        /* Call start before first fram updated */
        void Start()
        {
                offset = transform.position - player.transform.position;
        }

        /* Update position of the camera once per frame */
        void LateUpdate()
        {
                transform.position = player.transform.position + offset;
        }
}