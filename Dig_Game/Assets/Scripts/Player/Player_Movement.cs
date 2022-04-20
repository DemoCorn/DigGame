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
    [SerializeField] private float holdJumpMultiplier = 1.5f;

    private float mVerticalVelocity = 0.0f;
    private bool isGrounded = false;

    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private Animator animator;
    

    private int nDirection = 0;
    private bool isJumping = false;
    private bool maintainJump = false;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (mVerticalVelocity < 0.0f)
        {
            maintainJump = false;
        }

        if (maintainJump)
        {
            mVerticalVelocity += (gravity/ holdJumpMultiplier);
        }
        else
        {
            mVerticalVelocity += gravity;
        }

        // Jump Code
        if (isJumping)
        {
            mVerticalVelocity = jumpPower;
            isJumping = false;
        }

        // Vertical Collision
        if (IsCollidingWithBlock(new Vector2(speed * nDirection * Time.fixedDeltaTime, mVerticalVelocity * Time.fixedDeltaTime)))
        {
            float offset = 0.0f;
            float offsetDirection = 1.0f;
            if (mVerticalVelocity <= 0.0f)
            {
                offsetDirection = -1.0f;
            }

            while (!IsCollidingWithBlock(new Vector2(speed * nDirection * Time.fixedDeltaTime, (offset + (0.1f * offsetDirection)) * Time.fixedDeltaTime)))
            {
                offset += (0.1f * offsetDirection);
            }
            isGrounded = mVerticalVelocity <= 0.0f;
            mVerticalVelocity = offset;
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

        //Animation     
        animator.SetFloat("Speed", Mathf.Abs(nDirection));





        // Jump Code
        if (Input.GetKeyDown(inputs.jump) && isGrounded)
        {
            isJumping = true;
            maintainJump = true;
        }
        else if (!Input.GetKey(inputs.jump) && maintainJump)
        {
            maintainJump = false;
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