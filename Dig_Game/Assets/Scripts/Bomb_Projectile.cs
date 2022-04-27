using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Bomb_Projectile : MonoBehaviour
{
    [SerializeField] float damage = 10.0f;
    [SerializeField] float speed = 4.0f;
    [SerializeField] bool thrown;
    Vector3 launchOffset;

    void Start()
    {
        if (thrown) 
        {
            var direction = -transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);
        Destroy(gameObject, 5); //Destroy automatically after 5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        Inputs inputs = GameManager.Instance.GetInputs();
        if (Input.GetKeyDown((KeyCode)inputs.attack))
        {
            Instantiate()
        }

        if (!thrown)
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<Non_Player_Health>();
        if(enemy)
        {
            enemy.GetComponent<Non_Player_Health>().Hit(damage);
        }
    }
}
