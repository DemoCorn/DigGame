using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    // Variables
    private BoxCollider2D mAttackHitbox;
    public Vector2 rightAttackOffset = new Vector2(0.6f, 0.0f);
    public Vector2 rightAttackSize = new Vector2(0.2f, 1.0f);

    void Start()
    {
        mAttackHitbox = gameObject.AddComponent<BoxCollider2D>();
        mAttackHitbox.enabled = false;
    }

    void Update()
    {
        // Check each key direction to figure out if we should be attacking anywhere
        if (Input.GetKeyDown("right"))
		{
            SetAttackVariables(rightAttackOffset, rightAttackSize);
		}
        else if (Input.GetKeyDown("left"))
        {
            SetAttackVariables(new Vector2(-rightAttackOffset.x, rightAttackOffset.y), rightAttackSize);
        }
        else if (Input.GetKeyDown("up"))
        {
            SetAttackVariables(new Vector2(rightAttackOffset.y, rightAttackOffset.x), new Vector2(rightAttackSize.y, rightAttackSize.x));
        }
        else if (Input.GetKeyDown("down"))
        {
            SetAttackVariables(new Vector2(rightAttackOffset.y, -rightAttackOffset.x), new Vector2(rightAttackSize.y, rightAttackSize.x));
        }

        // Do attacking things

        mAttackHitbox.enabled = false;
    }

    private void SetAttackVariables(Vector2 offset, Vector2 size)
	{
        mAttackHitbox.offset = offset;
        mAttackHitbox.size = size;
        mAttackHitbox.enabled = true;
	}
}
