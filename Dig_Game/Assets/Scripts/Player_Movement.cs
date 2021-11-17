using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Movement : MonoBehaviour
{
    // Member Classes
    public float speed = 10.0f;
    public float jumpPower = 100.0f;
    public float gravity = -0.5f;
    private float mVerticalVelocity = 0.0f;
    private bool isGrounded = false;

    private BoxCollider2D mHitbox;
    public Vector2 HitboxSize = new Vector2(1.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        mHitbox = gameObject.AddComponent<BoxCollider2D>();
        mHitbox.size = HitboxSize;
    }
    void Update()
    {
        // Find if we should be going in the positive or negative direction
        int nDirection = (Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("a")));

        mVerticalVelocity += gravity;
        
        // Very basic jump code, should be changed once we have collision
        if (transform.position.y <= -4.0f)
        {
            mVerticalVelocity = 0.0f;
        }
        if (Input.GetKey("space") && (transform.position.y <= -4.0f || isGrounded))
        {
            mVerticalVelocity = jumpPower;
        }

        if (IsCollidingWithBlock(new Vector2(speed * nDirection * Time.deltaTime, 0.0f)))
		{
            nDirection = 0;
		}
        if (IsCollidingWithBlock(new Vector2(0.0f, mVerticalVelocity * Time.deltaTime)))
		{
            isGrounded = mVerticalVelocity <= 0.0f;
            mVerticalVelocity = 0.0f;
		}

        // Apply movement
        transform.Translate(speed * nDirection * Time.deltaTime, mVerticalVelocity * Time.deltaTime, 0.0f);
    }

    bool IsCollidingWithBlock(Vector2 offset)
	{
        List<Collider2D> collisions = new List<Collider2D>();

        mHitbox.offset = offset;

        int nCollisionCount = mHitbox.OverlapCollider(new ContactFilter2D(), collisions);

        // Go through all collided objects to damage blocks
        foreach (Collider2D collision in collisions)
        {
            if (collision.gameObject.tag == "BreakableBlock")
            {
                return true;
            }
        }

        mHitbox.offset = new Vector2(0.0f, 0.0f);
        return false;
    }
}
