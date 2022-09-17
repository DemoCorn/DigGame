using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public GameObject bossSprite;
    public GameObject boss;
    public ParticleSystem particles;
    public GameObject selfCheck;
    public GameObject canvas;
    public GameObject bossMinion1;

    //etai
    public AudioSource musicPlayer;
    public AudioClip bossMusic;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            particles.Play();
            boss.SetActive(true);
            bossSprite.SetActive(false);
            selfCheck.SetActive(false);
            canvas.SetActive(true);
            bossMinion1.SetActive(true);

            //etai
            musicPlayer.clip = bossMusic;
            musicPlayer.Play();
        }
    }
}
