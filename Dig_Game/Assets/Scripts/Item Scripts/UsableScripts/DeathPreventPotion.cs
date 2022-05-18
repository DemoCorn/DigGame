using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPreventPotion : UsableEffect
{
    float activeTime = 60.0f;
    float cooldownTime = 60.0f;

    override public float Activate()
    {
        GameManager.Instance.ActivatePlayerRevive(activeTime);
        return cooldownTime;
    }
}
