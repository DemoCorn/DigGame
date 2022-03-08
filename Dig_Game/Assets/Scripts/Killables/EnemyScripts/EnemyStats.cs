using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("General")]
    [Tooltip("Enemy's max health")]
    public float health;

    [Header("Attack Values")]
    public float damage = 10;
    public float attackDelay = 5;

    [Header("Movement Values")]
    public float speed = 200;
    public float jumpHeight = 0.5f;

    [Header("Pathfinding")]
    [Tooltip("The range the enemy will start to follow the player")]
    public float activateDistance = 50f;
    [Tooltip("The range the enemy will start to prompt attacks towards the player")]
    public float attackRange = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


