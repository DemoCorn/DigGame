using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPotion : UsableEffect
{
    public float attackBoost = 10.0f;
    override public void Activate()
    {
        GameManager.Instance.BuffPlayer(0.0f, 0.0f, attackBoost, 0.0f, 0.0f);
    }
}
