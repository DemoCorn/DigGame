using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyProjectiles : MonoBehaviour
{
    
    public enum ProjectileType {Direct, HeatSeeking}
    

    [Header("Settings")]
    public ProjectileType projectile;
    public float speed = 200f;
    public float seekingUpdateTime = 0.5f;
    public float attackDamage;
    
    //Private Variables
    private float nextWaypointDistance = 3f;
    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Rigidbody2D rb;
    private bool fired = true;
    private Vector2 force;

    private GameObject player;
    private Transform target;


    // Start is called before the first frame update
    private void Awake()
    {
        GetPlayer();
        gameObject.SetActive(true);
    }

    void Start()
    {
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(projectile == ProjectileType.HeatSeeking)
        {
            InvokeRepeating("UpdatePath", 0f, seekingUpdateTime);
        }
    }

    void FixedUpdate()
    {

        if (target == null)
            target = player.transform;
        

        switch (projectile)
        {
            case ProjectileType.Direct:
                ProjectileDirect();
                break;
            case ProjectileType.HeatSeeking:
                ProjectileHeatSeeking();                
                break;
        }
    }
    private void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    private void ProjectileDirect()
    {
        
        if (this == null) return;
        if(fired == true)
        {
        Vector2 direction = target.position - transform.position;
        force = direction * speed * Time.deltaTime;
        }
        rb.AddForce(force);
        fired = false;
    }
    private void ProjectileHeatSeeking()
    {
        if (this == null) return;
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count) return;  // Reached End of Path

        //Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 forceH = direction * speed * Time.deltaTime;

        //Movement
        rb.AddForce(forceH);

        //NextWaypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            
            currentWaypoint++;
        }
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            target = player.transform;
        }
    }

    private GameObject GetPlayer()
    {
        player = GameObject.FindWithTag("Player");
        return player;
    }
}
