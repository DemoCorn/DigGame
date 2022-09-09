using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCave : MonoBehaviour
{
    public AudioSource caveSound;
    public AudioSource music;

    public bool caveOn = false;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            GameManager.Instance.tutorialComplete = true;
            //music.Play();

            if (caveOn)
            {
                caveOn = false;
                StartCoroutine(FadeIn(caveSound, 2.0f));
                
            }

            else
            {
                caveOn = true;
                StartCoroutine(FadeOut(caveSound, 2.0f));
            }

        }
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
