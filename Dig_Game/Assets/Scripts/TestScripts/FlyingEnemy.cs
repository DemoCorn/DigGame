using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    private EnemyStates currentState;
    public GameObject player;
    public bool attackState = false;
    
    [Header("Setup")]    
    public Transform enemyStart;
    public Transform[] enemyPatrolPath;

    [Header("Enemy Values")]
    [SerializeField]
    private float health = 20;
    
    public float attackDamage = 12;
    [SerializeField]
    private float moveSpeed = 5;
    
    public float attackSpeed = 5;
    



    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
       
        if(attackState == true)
        {
            Chase();
            Flip();
        }
        else
        {
            ReturnToStart();
        }
            
       
    }

    public enum EnemyStates
    {
        attack,
        patrol,
        roaming,
        wait        
    }

    private void Chase()
    {   
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);   
    }
    private void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void ReturnToStart()
    {
        transform.position = Vector2.MoveTowards(transform.position, enemyStart.transform.position, moveSpeed * Time.deltaTime);
    }
   
}
