using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MovementV2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CapsuleCollider2D boxCollider;
    [SerializeField] private new Rigidbody2D rigidbody;
    //[SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

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
      //  animator.SetBool("Jumping", !IsGrounded());


        //Movement
        rigidbody.velocity = new Vector2(movementSpeed * Input.GetAxisRaw("Horizontal"), rigidbody.velocity.y);
        float speed = Mathf.Abs(rigidbody.velocity.x);
        //animator.SetFloat("Speed", speed);

        if (rigidbody.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rigidbody.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size / 2, 0, Vector2.down, .4f, platformLayerMask);
        Debug.Log(hit.collider);
        return hit.collider != null;
    }
}