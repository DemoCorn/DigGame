using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPosition;

    public float cooldownTimer;
    public float cooldownRequirement;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = 0;
        SpawnProjectile();
        //spawnPosition = GameObject.FindGameObjectWithTag("Boss").transform;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= cooldownRequirement)
        {
            SpawnProjectile();
            cooldownTimer = 0;
        }

    }
    public void SpawnProjectile()
    {
        Instantiate(projectile, spawnPosition.position, spawnPosition.rotation);
    }
}
