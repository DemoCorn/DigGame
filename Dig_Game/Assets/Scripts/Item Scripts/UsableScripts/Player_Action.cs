using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action : UsableEffect
{
    [SerializeField] Bomb_Projectile bomb;

    Player_Action()
    {
        cooldownTime = 1.0f;
    }

    override public float Activate()
    {
        Attack();
        return cooldownTime;
    }

    public void Attack()
    {
        Instantiate(bomb, GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("Player").transform.rotation);        
    }


}
