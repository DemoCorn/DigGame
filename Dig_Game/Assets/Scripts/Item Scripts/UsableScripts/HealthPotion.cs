using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : UsableEffect
{
    public float heal = 25.0f;
    override public void Activate()
    {
        GameManager.Instance.HealPlayer(heal);
    }
}
