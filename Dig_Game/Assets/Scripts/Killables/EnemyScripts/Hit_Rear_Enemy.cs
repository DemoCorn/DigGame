using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Rear_Enemy : MonoBehaviour
{
    [HideInInspector] public bool isHit;
    public Non_Player_Health health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        isHit = true;
        //Debug.Log("REAR HIT");

        health.mImmune = false;
    }
}
