using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public GameObject bossSprite;
    public GameObject boss;
    public ParticleSystem particles;
    public GameObject selfCheck;
    public GameObject darkness;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            particles.Play();
            boss.SetActive(true);
            bossSprite.SetActive(false);
            selfCheck.SetActive(false);
            darkness.SetActive(true);
        }
    }
}
