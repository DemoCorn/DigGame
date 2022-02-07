using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Vector3 GetPosition() { return transform.position; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
