using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableEffect : MonoBehaviour
{
    virtual public void Activate()
    {
        Debug.LogWarning("Usable Item should not use usable effect script directly");
    }
}
