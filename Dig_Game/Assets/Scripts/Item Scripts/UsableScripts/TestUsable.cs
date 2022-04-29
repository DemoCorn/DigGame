using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUsable : UsableEffect
{
    override public float Activate()
    {
        Debug.LogWarning("Test");
        return 0.0f;
    }
}
