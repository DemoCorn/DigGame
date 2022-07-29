using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefencePotion : UsableEffect
{
    public float defenceBoost = 25.0f;
    float activeTime = 60.0f;
    public new float  cooldownTime = 60.0f;

    override public float Activate()
    {
        GameManager.Instance.BuffPlayer(0.0f, defenceBoost, 0.0f, 0.0f, 0.0f, activeTime);
        return cooldownTime;
    }
}
