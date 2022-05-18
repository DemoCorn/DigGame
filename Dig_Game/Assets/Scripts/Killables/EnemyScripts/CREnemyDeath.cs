using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CREnemyDeath : MonoBehaviour
{
    private CRManager cRManager;

    private void Awake()
    {
        cRManager = GetComponentInParent<CRManager>();
    }

    private void OnDestroy()
    {
        cRManager.enemyCount -= 1;
    }
}
