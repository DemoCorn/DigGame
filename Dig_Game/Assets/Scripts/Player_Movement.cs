using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Movement : MonoBehaviour
{
    // Member Classes
    public float mSpeed = 10.0f;
    private float mJumpPower = 100.0f;
    private const float mGravity = -0.5f;
    private float mVerticalVelocity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Find if we should be going in the positive or negative direction
        int nDirection = (Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("a")));

        mVerticalVelocity += mGravity;

        if (transform.position.y <= -4.0f)
        {
            mVerticalVelocity = 0.0f;
        }

        if (Input.GetKey("w") && transform.position.y <= -4.0f)
        {
            mVerticalVelocity = mJumpPower;
        }
        
        transform.Translate(mSpeed * nDirection * Time.deltaTime, mVerticalVelocity * Time.deltaTime, 0.0f);
    }
}
