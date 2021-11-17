using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_MovementV2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private new Rigidbody2D rigidbody;

    [Header("Stats")]
    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float jumpForce = 10;

    [Header("Layers")]
    [SerializeField] private LayerMask platformLayerMask;

    [SerializeField] private float gravity = -0.1f;
    [SerializeField] private float mVerticalVelocity = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        mVerticalVelocity += gravity;
        //Jump
        //if (isColliding(Vector2.up) || isColliding(Vector2.down))
        if (isColliding(Vector2.down, new Vector3(0.0f, mVerticalVelocity, 0.0f)))
        {
            mVerticalVelocity = 0.0f;
        }

        if (isColliding(Vector2.down, Vector3.zero) && Input.GetKey("space"))
        {
            mVerticalVelocity = jumpForce;
        }

        // Find if we should be going in the positive or negative direction
        int nDirection = (Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("a")));

        //Movement
        transform.Translate(movementSpeed * nDirection * Time.deltaTime, mVerticalVelocity * Time.deltaTime, 0.0f);
    }

    public bool isColliding(Vector2 direction, Vector3 offset)
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + offset, boxCollider.bounds.size / 2, 0, direction, .1f, platformLayerMask);
        //Debug.Log(hit.collider);
        return hit.collider != null;
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size / 2, 0, Vector2.down, .4f, platformLayerMask);
        //Debug.Log(hit.collider);
        return hit.collider != null;
    }
}