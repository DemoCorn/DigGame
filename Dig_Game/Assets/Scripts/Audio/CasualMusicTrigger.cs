using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CasualMusicTrigger : MonoBehaviour
{
    public AudioSource miningMusic;
    public AudioSource combatMuted;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            miningMusic.Play();
            combatMuted.Play();

            gameObject.SetActive(false);
        }
    }
}
