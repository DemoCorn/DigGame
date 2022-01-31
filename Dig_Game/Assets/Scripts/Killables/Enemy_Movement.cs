using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    
    [SerializeField] private float speed = 10.0f;
    private int mDirection;
    [SerializeField] private float gravity = -0.5f;

    private float mVerticalVelocity = 0.0f;

    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private LayerMask platformLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        mDirection = (speed < 0) ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        mVerticalVelocity += gravity;

        // Collision With Ground
        if (IsCollidingWithBlock(new Vector2(speed * Time.deltaTime, mVerticalVelocity * Time.deltaTime)))
        {
            mVerticalVelocity = 0.0f;
        }

        // Swap Direction when a wall is hit
        if (IsCollidingWithBlock(new Vector2(speed * Time.deltaTime + (0.08f * mDirection), mVerticalVelocity * Time.deltaTime)))
        {
            speed = -speed;
            mDirection = -mDirection;
        }

        // Apply movement
        transform.Translate(speed * Time.deltaTime, mVerticalVelocity * Time.deltaTime, 0.0f);
    }

    bool IsCollidingWithBlock(Vector2 offset)
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
