using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : UsableEffect
{
    public float heal = 25.0f;
    float cooldownTime = 60.0f;

    override public float Activate()
    {
        GameManager.Instance.HealPlayer(heal);
        return cooldownTime;
    }
}