using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperySurface : MonoBehaviour
{
    [SerializeField] private float SlipperyCoefficeint;
    private BoxCollider2D BoxColl;

    private void Start()
    {
        BoxColl=GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        ///player Speed to slipperbox collis


    }
    














}
