using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Attack : MonoBehaviour
{
    public Projectile shotToFire;
    public Transform shotPoint;

    int bulletsAvailable = 5;
    float reloadTimer = 0;

    //cooldown
    public float cd = 1.5f;
    private float nextFireTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        if (bulletsAvailable <= 0)
        {
            reloadTimer += Time.deltaTime;


        }

        if (Time.time > nextFireTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Vector2 moveDirection = (cursorLocation - (Vector2)transform.position).normalized;
                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = moveDirection;

                nextFireTime = Time.time + cd;
            }
           

        }
    }
}
