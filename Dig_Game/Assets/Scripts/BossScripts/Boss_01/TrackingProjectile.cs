using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TrackingProjectile : MonoBehaviour
{
    public float proSpeed = 5f;
    public float rotateSpeed = 200f;
    public Transform player;
    private Rigidbody2D rb;
    public Transform spawnPosition;

    public float cooldownTimer;
    public float cooldownRequirement;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cooldownTimer = 0;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)player.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity =  - rotateAmount * rotateAmount * rotateSpeed;
        rb.velocity = transform.up * proSpeed;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= cooldownRequirement)
        {
            SpawnProjectile();
            cooldownTimer = 0;
        }
    }


    private void OnTriggerEnter2D(Collider other)
    {
      
    }


    public void SpawnProjectile()
    {
        Instantiate(rb, spawnPosition.position, spawnPosition.rotation);
    }
}
