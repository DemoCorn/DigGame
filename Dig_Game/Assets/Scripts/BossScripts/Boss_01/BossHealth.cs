using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public static float bossMaxHP = 100;
    public static float bossHP;

    public bool isStunned;

    public float stunTimer;
    public float stunRequirement;

    // Start is called before the first frame update
    void Start()
    {
        stunTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned == true)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer <= stunRequirement)
            {
                stunTimer = 0;
                //player can now damage

                if (stunTimer == stunRequirement)
                {
                    isStunned = false;
                }

            }
        }
    }
}
