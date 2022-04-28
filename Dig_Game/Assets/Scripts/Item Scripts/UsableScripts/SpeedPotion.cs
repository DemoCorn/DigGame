using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : UsableEffect
{
    public float speedBoost = 5.0f;
    override public void Activate()
    {
        GameManager.Instance.BuffPlayer(0.0f, 0.0f, 0.0f, 0.0f, speedBoost);
    }
}
