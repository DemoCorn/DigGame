using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefencePotion : UsableEffect
{
    public float defenceBoost = 25.0f;
    override public void Activate()
    {
        GameManager.Instance.BuffPlayer(0.0f, defenceBoost, 0.0f, 0.0f, 0.0f);
    }
}
