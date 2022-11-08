using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
 public AudioMixer mix;

 public void SetLevel (float slideVal)
 {
    if (mix != null)
    {
        mix.SetFloat("MusicVolume", Mathf.Log10(slideVal) * 20);
    }
 }
}