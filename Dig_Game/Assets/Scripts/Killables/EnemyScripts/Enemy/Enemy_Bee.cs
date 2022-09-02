using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : Enemy
{
    public Rigidbody2D rbBee;

    public override void Init()
    {
        base.Init();
        rbBee = GetComponent<Rigidbody2D>();
    }


}
