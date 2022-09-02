using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy_V1 : Enemy, IDamageable
{
    public void GetHit(float damage)
    {
        health -= damage;
        if(health < 1)
        {
            health = 0;
            isDeadEnemy = true;
        }
        enemyAnim.SetTrigger("hit");

        
    }

    
    
}
