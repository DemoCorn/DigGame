using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    
    int direction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > collision.transform.position.x )
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player get hurt");
            collision.GetComponent<IDamageable>().GetHit(1);
        }
    }
    
}
