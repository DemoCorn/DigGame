using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Bomb_Projectile : MonoBehaviour
{
    [SerializeField] float damage = 50.0f;
    [SerializeField] float speed = 4.0f;
    [SerializeField] bool thrown;
    public Vector3 launchOffset;

    public float fieldOfImpact;
    public float force;
    public LayerMask layerToHit;

    public GameObject ExplosionEffect;
    private GameObject player;
    bool isFacingLeft;


    void Start()
    {
        isFacingLeft = GameObject.FindWithTag("Player").GetComponent<Player_Movement>().isFacingLeft;
        if (thrown) 
        {
            if(isFacingLeft)
            { 
                var direction = -transform.right + Vector3.up;
                GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
            }
            else
            {
                var direction = transform.right + Vector3.up;
                GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
            }
        }
        if (isFacingLeft)
        {
            transform.Translate(launchOffset);
        }
        else
        {
            transform.Translate(-launchOffset.x, launchOffset.y, launchOffset.z);
        }
        Destroy(gameObject, 5); //Destroy automatically after 5 seconds
    }

    private void OnDestroy()
    {
        Explode();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Non_Player_Health>() != null)
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);

        foreach(Collider2D obj in objects)
        {
            if (obj.GetComponent<Non_Player_Health>() != null)
            {
                obj.GetComponent<Non_Player_Health>().Hit(damage);
            }
        }
        GameObject ExplosionEffectPlay = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEffectPlay, 10);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
