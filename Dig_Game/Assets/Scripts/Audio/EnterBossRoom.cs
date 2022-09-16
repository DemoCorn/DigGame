using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnterBossRoom : MonoBehaviour
{
    public AudioSource caveSound;
    public AudioSource musicPlayer;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            StartCoroutine(FadeOut(musicPlayer, 1.0f));
            StartCoroutine(FadeIn(caveSound, 1.0f));

            gameObject.SetActive(false);
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
        float startVolume = 1.0f;

        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        //audioSource.Stop();
        audioSource.volume = startVolume;
    }

    /*public bool caveOn = false;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            GameManager.Instance.tutorialComplete = true;
            //music.Play();

            if (caveOn)
            {
                caveOn = false;
                

            }

            else
            {
                caveOn = true;
                StartCoroutine(FadeOut(caveSound, 1.0f));
            }

        }
    }

    

    */
}
