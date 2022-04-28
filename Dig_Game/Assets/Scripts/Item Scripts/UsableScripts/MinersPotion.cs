using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersPotion : UsableEffect
{
    public float minerBoost = 10.0f;
    override public void Activate()
    {
        GameManager.Instance.BuffPlayer(0.0f, 0.0f, 0.0f, minerBoost, 0.0f);
    }
}
