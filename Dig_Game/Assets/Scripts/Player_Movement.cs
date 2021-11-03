using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Movement : MonoBehaviour
{
    // Member Classes
    public float mSpeed = 10.0f;
    public float mJumpPower = 100.0f;
    public float mGravity = -0.5f;
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
        
        // Very basic jump code, should be changed once we have collision
        if (transform.position.y <= -4.0f)
        {
            mVerticalVelocity = 0.0f;
        }
        if (Input.GetKey("space") && transform.position.y <= -4.0f)
        {
            mVerticalVelocity = mJumpPower;
        }
        
        // Apply movement
        transform.Translate(mSpeed * nDirection * Time.deltaTime, mVerticalVelocity * Time.deltaTime, 0.0f);
    }
}
