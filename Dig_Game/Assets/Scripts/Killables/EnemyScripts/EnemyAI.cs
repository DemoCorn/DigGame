using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Animations;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Typing")]
    [Tooltip("How this enemy will be moving")]
    public MovementType movement;
    [Tooltip("How this enemy will attack")]
    public AttackType attackType;

    private float attackDelay = 3f;
    public GameObject spawnOffset;
    public GameObject damageField;

    [Header("Custom Behaviour")]
    [Tooltip("If turned on the enemy will follow the player when they're within a specified range.")]
    public bool followEnabled = true;
    [Tooltip("If turned on the enemy will jump if the player is above them")]
    public bool jumpEnabled = true;
    [Tooltip("If turned on the enemy will turn to face the player's direction")]
    public bool directionLookEnabled = true;

    [Header("Physics")]    
    private float speed = 200f;    
    private float jumpModifier = 0.3f;    
    private float jumpHeightRequirement = 0.8f;
    private float groundCheckOffset = 0.1f;

    [Header("Pathfinding")]
    private GameObject player;
    private Transform target;
    [Tooltip("The range the enemy will start to follow the player")]
    private float activateDistance = 50f;
    [Tooltip("The range the enemy will start to prompt attacks towards the player")]
    private float attackRange = 10f;
    private static Vector3 startingPosition;
    private Vector3 roamingPosition;
    private float nextWaypointDistance = 3f;
    private float pathUpdateSeconds = 0.5f;
    private float waitTimer = 0;

    private Path path;
    private int currentWaypoint = 0;
    private bool isGrounded = false;
    private bool canAttack = false;
    public enum MovementType {Flying, Ground}
    public enum AttackType {Melee, Ranged}
    private enum BehaviourStates
    {
        Roam, 
        Wait, 
        Follow,
        retreat,
    }
    private BehaviourStates behaviour;

    private Seeker seeker;
    private Rigidbody2D rb;
    [Header("Graphics")]
    public GameObject enemyGFX;
    

    private EnemyStats enemyStats;

    private void Awake()
    {
        GetPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        /**Startup**/{
            GetStats();
            //GetGFX();
            
            
            damageField.SetActive(false);
            behaviour = BehaviourStates.Roam;
            seeker = GetComponent<Seeker>();
            path = GameObject.Find("A *").GetComponent<Path>();
            rb = GetComponent<Rigidbody2D>();
            startingPosition = transform.position;
            canAttack = true;

            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.GetSwordCollider(), true);
        }

        //Update Path every set amount of seconds
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }
    private void Update()
    { 
        {
            EnemyGFX();
            SetSpawnOffset();
        }
        //Debug.Log(player.name);

        //Attack if player is within range
        if (canAttack == true && TargetInAttackRange())
        {
            Attack();
        }

        //Debug.Log(behaviour);
    }
    void FixedUpdate()
    {
        if (target == null)
            target = player.transform;

        /**Enemy set to wait behavior during attack**/
        if (canAttack == false)
        {
            behaviour = BehaviourStates.Wait;
            waitTimer += 1 * Time.fixedDeltaTime;
            //Debug.Log(waitTimer);
        }

        /**Enemy set to roam if the player in not within distance and 
        the enemy is not currently waiting**/
        if (Vector2.Distance(transform.position, target.transform.position) > activateDistance&& waitTimer >= attackDelay)
        {
            behaviour = BehaviourStates.Roam;  
        }

        /**Enemy set to follow player if they're within distance and 
        the enemy is not currently waiting**/
        if (followEnabled && TargetInDistance() && waitTimer >= attackDelay)
        {
            behaviour = BehaviourStates.Follow;
        }

        if (movement == MovementType.Ground)
        {
            PathFindingGround();
        }
        else if (movement == MovementType.Flying){
            PathFindingFlying();
        }
    }
    private void UpdatePath()
    {
        
       //Control Pathfinding depending on the current behaviour state
       switch (behaviour)
        {
            case BehaviourStates.Roam:                            
                if (seeker.IsDone())
                {
                    if (movement == MovementType.Flying) roamingPosition = GetRoamingPositionXY();
                    else if (movement == MovementType.Ground) roamingPosition = GetRoamingPositionX();
                    seeker.StartPath(rb.position, roamingPosition, OnPathComplete);
                }         
                break;
            case BehaviourStates.Wait:
                if(seeker.IsDone())
                    seeker.StartPath(rb.position, rb.position, OnPathComplete);                
                break;
            case BehaviourStates.Follow:
                if (seeker.IsDone())
                {
                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                }                               
                break;
            case BehaviourStates.retreat:
                if (seeker.IsDone())
                {
                    seeker.StartPath(rb.position, startingPosition, OnPathComplete);
                }
                break;
            default:
                break;
        }
    }


    /************************Startup*******************************/
    private void GetStats()
    {
        enemyStats = GetComponent<EnemyStats>();
        attackDelay = enemyStats.attackDelay;
        speed = enemyStats.speed;
        jumpModifier = enemyStats.jumpHeight;
    }
    private void GetGFX()
    {
        enemyGFX = GetComponentInChildren<GameObject>();
    }

    //PathFinding Behavior
    private GameObject GetPlayer() // Is finding player
    {
        player = GameObject.FindWithTag("Player");
        
        return player;
    }


    private static Vector3 GetRoamingPositionXY()
    {
        Vector3 direction = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        return  startingPosition + direction * Random.Range(1f, 5f);
    } //Get Roaming position for flying AI
    private static Vector3 GetRoamingPositionX()
    {
        Vector3 direction = new Vector3(Random.Range(-1, 1), 0).normalized;
        return startingPosition + direction * Random.Range(10f, 50f);
    } //Get Roaming position for flying Walkning AI
    private void PathFindingFlying()
    {
        //Debug.Log("I am flying");
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count) return;  // Reached End of Path

        //Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //Movement
        rb.AddForce(force);

        //NextWaypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    } //Flying Pathingfinding behavior
    private void PathFindingGround()
    {
        //Debug.Log("I am walking!");
        if (path == null) {return;}
        if (currentWaypoint >= path.vectorPath.Count) { return; } // Reached End of Path

        //See if Colliding With Anything
        isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + groundCheckOffset);

        //Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //Jump
        if(jumpEnabled && isGrounded)
        {
            if(direction.y > jumpHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        //Movement
        rb.AddForce(force);

        //NextWaypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    } //Walking Pathingfinding behavior

    //EnemyAttackingBehaviour
    private void Attack()
    {        
        
        if (attackType == AttackType.Melee)
        {
            if(canAttack == true)
            {
                Invoke("MeleeAttack", attackDelay);                
                canAttack = false;
            }
            
        }
        else if (attackType == AttackType.Ranged)
        {
            if(canAttack == true)
            {
                Invoke("RangedAttack", attackDelay);
                canAttack = false;
            }
        }        
    }
    //Melee Attack
    private void MeleeAttack()
    {
        
        damageField.SetActive(true);
        Invoke("MeleeReset", 1f);
    }
    private void MeleeReset()
    {
        damageField.SetActive(false);
        canAttack = true;
    }
    //Ranged Attack
    void RangedAttack()
    {                
        damageField.SetActive(true);
        Transform spawnTransform = spawnOffset.transform;
        Instantiate(damageField, new Vector3 (spawnTransform.position.x, spawnTransform.position.y, spawnTransform.transform.position.z), Quaternion.identity , gameObject.transform);
        Invoke("RangedReset", 1f);
    }
    private void RangedReset ()
    {
        canAttack = true;
        if(waitTimer >= attackDelay)
        {
            waitTimer = 0f;
        }
    }

    //Distance Checking
    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }
    private bool TargetInAttackRange()
    {
        return Vector2.Distance(transform.position, target.transform.position) < attackRange;
    }

    //Flip GFX && Damage field spawn location
    private void EnemyGFX()
    {
        if (target.position.x >= transform.position.x)
        {
            enemyGFX.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (target.position.x <= -transform.position.x)
        {
            enemyGFX.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void SetSpawnOffset()
    {
        if (target.position.x >= transform.position.x)
        {
            spawnOffset.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, 0);
        }
        else if (target.position.x <= -transform.position.x)
        {
            spawnOffset.transform.position = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y, 0);            
        }
    }

    //Path Complete
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }



}
