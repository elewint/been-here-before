using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFade : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRB;
    private ParticleSystem ps;
    private List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        player = GameObject.Find("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        if (ps == null)
            return;

        // Debug.Log(ps);
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        // Debug.Log("particles entered: " + numEnter);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = inside[i];
            // p.startColor = new Color32(255, 255, 255, 50);
            // Color32 currColor = p.GetCurrentColor(ps);
            // currColor.a = 50;
            // p.startColor = currColor;
            // float velX = playerRB.velocity.x * 1.2f;
            // float velY = playerRB.velocity.y * 1.2f;
            // p.velocity = new Vector3(velX, velY, 0);
            p.startColor = new Color32(255, 255, 255, 50);
            p.remainingLifetime = 0.5f;
            inside[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
    }
}
