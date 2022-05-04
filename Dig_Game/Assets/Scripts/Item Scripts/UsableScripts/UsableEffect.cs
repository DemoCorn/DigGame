using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableEffect : MonoBehaviour
{
    virtual public float Activate()
    {
        Debug.LogWarning("Usable Item should not use usable effect script directly");
        return 0.0f;
    }
}
