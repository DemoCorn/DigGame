using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : UsableEffect
{
    public float speedBoost = 5.0f;
    float activeTime = 60.0f;

    SpeedPotion()
    {
        cooldownTime = 60.0f;
    }

    override public float Activate()
    {
        GameManager.Instance.BuffPlayer(0.0f, 0.0f, 0.0f, 0.0f, speedBoost, activeTime);
        return cooldownTime;
    }
}
