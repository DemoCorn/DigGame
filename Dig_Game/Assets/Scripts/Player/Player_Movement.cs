using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Movement : MonoBehaviour
{

    // Member Classes
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpPower = 100.0f;
    [SerializeField] private float gravity = -0.5f;

    private float mVerticalVelocity = 0.0f;
    private bool isGrounded = false;

    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private LayerMask platformLayerMask;

    private int nDirection = 0;
    private bool isJumping = false;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        mVerticalVelocity += gravity;

        // Jump Code
        if (isJumping)
        {
            mVerticalVelocity = jumpPower;
            isJumping = false;
        }

        // Vertical Collision
        if (IsCollidingWithBlock(new Vector2(speed * nDirection * Time.fixedDeltaTime, mVerticalVelocity * Time.fixedDeltaTime)))
        {
            isGrounded = mVerticalVelocity <= 0.0f;
            mVerticalVelocity = 0.0f;
        }
        else
        {
            isGrounded = false;
        }

        // Horizontal Collision
        if (IsCollidingWithBlock(new Vector2(speed * nDirection * Time.fixedDeltaTime + (0.08f * nDirection), mVerticalVelocity * Time.fixedDeltaTime)))
        {
            nDirection = 0;
        }

        // Apply movement
        transform.Translate(speed * nDirection * Time.fixedDeltaTime, mVerticalVelocity * Time.fixedDeltaTime, 0.0f);
    }

    void Update()
    {
        Inputs inputs = GameManager.Instance.GetInputs();
        // Check win condition
        if (transform.position.y < GameManager.Instance.LayerManager.GetLevelHeight().Key - 5)
        {
            GameManager.Instance.EndGame(true);
        }

        // Find if we should be going in the positive or negative direction
        nDirection = (Convert.ToInt32(Input.GetKey(inputs.right)) - Convert.ToInt32(Input.GetKey(inputs.left)));

        // Jump Code
        if (Input.GetKey(inputs.jump) && isGrounded)
        {
            isJumping = true;
        }
    }

    private bool IsCollidingWithBlock(Vector2 offset)
    {
        List<Collider2D> collisions = new List<Collider2D>();

        hitbox.offset = offset;

        int nCollisionCount = hitbox.OverlapCollider(new ContactFilter2D(), collisions);

        // Go through all collided objects to see if the new position would intersect with a block
        foreach (Collider2D collision in collisions)
        {
            if ((platformLayerMask.value & (1 << collision.gameObject.layer)) > 0)
            {
                return true;
            }
        }

        hitbox.offset = new Vector2(0.0f, 0.0f);
        return false;
    }
}