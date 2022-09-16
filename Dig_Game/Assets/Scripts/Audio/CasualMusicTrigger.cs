using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CasualMusicTrigger : MonoBehaviour
{
    public AudioSource miningMusic;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            miningMusic.Play();

            gameObject.SetActive(false);
        }
    }
}
