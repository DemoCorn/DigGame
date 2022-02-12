using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TrackingProjectile : MonoBehaviour
{
    public float proSpeed;
    public float rotateSpeed;
    public Transform player;
    

    private Rigidbody2D rb;

    public float cooldownTimer;
    public float cooldownRequirement;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cooldownTimer = 0;

       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)player.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity =  -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * proSpeed;
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= cooldownRequirement)
        {
            Destroy(gameObject);
            cooldownTimer = 0;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == ("Player"))
        {
            //Damage Player
            Destroy(gameObject);
        }  
       
    }
    

}
