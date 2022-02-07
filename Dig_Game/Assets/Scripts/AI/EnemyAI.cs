using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // ENEMY STATES
    private enum State
    {
        Roaming,
        ChaseTarget,
        AttackTarget,
        GoBackToStart
    }

    private IAimShootAnims aimShootAnims;
    private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float nextAttackTime; 
    private State state;

    private void Awake()
    {
        pathfindingMovement = GetComponent<EnemyPathfindingMovement>();
        aimShootAnims = GetComponent<IAimShootAnims>();
        state = State.Roaming;
    }
    void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        switch (state)
        {
            default:
             // ROAMING
             case State.Roaming:
                    // Move Randomly
                pathfindingMovement.MoveToTimer(roamPosition);
                float reachedPositionDistance = 10f;
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    // Reached Roam Position
                    roamPosition = GetRoamingPosition();
                }
                    // Try to find a target
                FindTarget();
                break;
            // CHASE TARGET
            case State.ChaseTarget:
                // Move towards target
                pathfindingMovement.MoveToTimer(Player.Instance.GetPosition());
                // Aim at target
                aimShootAnims.SetAimTarget(Player.Instance.GetPosition());

                float attackRange = 5f;
                if (Vector3.Distance(transform.position, Player.Instance.GetPosition()) < attackRange)
                {
                    // Target is within attack range
                    if(Time.time > nextAttackTime)
                    {
                        //Stop Moving to attack
                        pathfindingMovement.StopMoving();
                        //Attack target (put state to a dummy state so it can finish the attack animation)
                        state = State.AttackTarget;
                        // EX - Finish ShootTarget and its animation, then go back to the chase target state
                        aimShootAnims.ShootTarget(Player.Instance.GetPosition(), () => { state = State.ChaseTarget; });
                        float fireRate = .5f;// PROBABLY SHOULD BE INSIDE A WEAPON CLASS BELONGING TO ENEMY WITH ATTACK RATE
                        nextAttackTime = Time.time + fireRate; 
                    }
                }

                // Stop chasing if too far
                float stopChaseDistance = 80f;
                if(Vector3.Distance(transform.position, Player.Instance.GetPosition()) > stopChaseDistance)
                {
                    state = State.GoBackToStart;
                }
                break;
            // ATTACK STATE - Dummy state for attack animation / action to fully complete without interruption;
            case State.AttackTarget:                
                break;
            // GO BACK TO START
            case State.GoBackToStart:
                    // Set path back to starting position
                pathfindingMovement.MoveToTimer(startingPosition);

                reachedPositionDistance = 10f;
                if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance)
                {
                    // Reached Start Position, now start roaming again
                    state = State.Roaming;
                }
                break;
        }


        
    }

    private Vector3 GetRoamingPosition()
    {
        // Current position + Random Direction * Random Distance
       return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }

    private void FindTarget()
    {
        float targetRange = 50f;
        if (Vector3.Distance (transform.position, Player.Instance.GetPosition()) < targetRange )
        {
            // Player is within target range
            state = State.ChaseTarget;
        }
    }


    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}