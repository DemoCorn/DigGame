using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral_Logic : MonoBehaviour
{
    // Attach to mineral Object
    void OnDisable()
    {
        GameManager.Instance.AddScore(1);
    }
}
