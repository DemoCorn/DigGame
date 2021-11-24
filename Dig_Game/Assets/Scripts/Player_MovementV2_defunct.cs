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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }

        //Movement
        int nDirection = (Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("a")));
        rigidbody.velocity = new Vector2(movementSpeed * nDirection, rigidbody.velocity.y);
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size / 2, 0, Vector2.down, .4f, platformLayerMask);
        //Debug.Log(hit.collider);
        return hit.collider != null;
    }
}