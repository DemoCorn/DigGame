using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    // Variables
    [SerializeField] float blockDamage = 15.0f;
    [SerializeField] float enemyDamage = 15.0f; 

    private BoxCollider2D mAttackHitbox;
    [SerializeField] Vector2 rightAttackOffset = new Vector2(0.6f, 0.0f);
    [SerializeField] Vector2 rightAttackSize = new Vector2(0.2f, 1.0f);

    void Start()
    {
        mAttackHitbox = gameObject.AddComponent<BoxCollider2D>();
        mAttackHitbox.enabled = false;
    }

    void Update()
    {
        Inputs inputs = GameManager.Instance.GetInputs();
        // Check each key direction to figure out if we should be attacking anywhere
        if (Input.GetKeyDown(inputs.rAttack))
		{
            SetAttackVariables(rightAttackOffset, rightAttackSize);
		}
        else if (Input.GetKeyDown(inputs.lAttack))
        {
            SetAttackVariables(new Vector2(-rightAttackOffset.x, rightAttackOffset.y), rightAttackSize);
        }
        else if (Input.GetKeyDown(inputs.uAttack))
        {
            SetAttackVariables(new Vector2(rightAttackOffset.y, rightAttackOffset.x), new Vector2(rightAttackSize.y, rightAttackSize.x));
        }
        else if (Input.GetKeyDown(inputs.dAttack))
        {
            SetAttackVariables(new Vector2(rightAttackOffset.y, -rightAttackOffset.x), new Vector2(rightAttackSize.y, rightAttackSize.x));
        }

        // Check if attack hitbox is active to skip some execution if it's not
        if (mAttackHitbox.enabled)
        {
            List<Collider2D> collisions = new List<Collider2D>();

            int nCollisionCount = mAttackHitbox.OverlapCollider(new ContactFilter2D(), collisions);
            GameObject collisionObject;

            // Go through all collided objects to damage blocks
            foreach (Collider2D collision in collisions)
            {
                collisionObject = collision.gameObject;
                if (collisionObject.GetComponent<Non_Player_Health>() != null)
                {
                    // Need either a better way to do prefab loading, or a better way to check if an object should use enemy damage
                    if (collisionObject.tag == "Enemy")
                    {
                        collisionObject.GetComponent<Non_Player_Health>().Hit(enemyDamage);
                    }
                    else
                    {
                        collisionObject.GetComponent<Non_Player_Health>().Hit(blockDamage);
                    }
                }
            }
            mAttackHitbox.enabled = false;
        }
    }

    private void SetAttackVariables(Vector2 offset, Vector2 size)
	{
        mAttackHitbox.offset = offset;
        mAttackHitbox.size = size;
        mAttackHitbox.enabled = true;
	}
}
