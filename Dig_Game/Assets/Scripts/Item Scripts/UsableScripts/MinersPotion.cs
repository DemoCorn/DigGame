using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersPotion : UsableEffect
{
    public float minerBoost = 10.0f;
    float activeTime = 60.0f;
    float cooldownTime = 60.0f;

    override public float Activate()
    {
        GameManager.Instance.BuffPlayer(0.0f, 0.0f, 0.0f, minerBoost, 0.0f, activeTime);
        return cooldownTime;
    }
}
