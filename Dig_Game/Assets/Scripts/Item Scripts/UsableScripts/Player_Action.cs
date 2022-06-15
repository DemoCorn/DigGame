using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action : UsableEffect
{
    [SerializeField] Bomb_Projectile bomb;
    float cooldownTime = 1.0f;
    private int bombsAmmo = 0;

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
