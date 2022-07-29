using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : UsableEffect
{
    public float heal = 25.0f;
    public new float cooldownTime = 1.0f;

    override public float Activate()
    {
        GameManager.Instance.HealPlayer(heal);
        return cooldownTime;
    }
}
