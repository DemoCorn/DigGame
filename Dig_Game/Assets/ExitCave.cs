using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCave : MonoBehaviour
{
    public AudioSource caveSound;
    public AudioSource music;

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Entered trigger");

        music.Play();

        StartCoroutine(AudioFadeOut.FadeOut(caveSound, 1.0f));
    }
}
