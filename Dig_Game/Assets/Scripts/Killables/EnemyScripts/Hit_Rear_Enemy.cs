using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Rear_Enemy : MonoBehaviour
{
    [HideInInspector] public bool isHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        isHit = true;
        //Debug.Log("REAR HIT");

        transform.parent.GetComponent<Non_Player_Health>().mImmune = false;
    }
}
