using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUsable : UsableEffect
{
    override public void Activate()
    {
        Debug.LogWarning("Test");
    }
}
