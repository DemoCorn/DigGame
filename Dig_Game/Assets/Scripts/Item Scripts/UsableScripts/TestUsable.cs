using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUsable : UsableEffect
{
    public new float cooldownTime = 0.0f;
    override public float Activate()
    {
        Debug.LogWarning("Test");
        return cooldownTime;
    }
}
