using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    // Variables
    [SerializeField] float enemyDamage = 1.0f; 

    private BoxCollider2D mAttackHitbox;
    [SerializeField] Vector2 rightAttackOffset = new Vector2(0.6f, 0.0f);
    [SerializeField] Vector2 rightAttackSize = new Vector2(0.2f, 1.0f);
    [SerializeField] Player_WeaponStats stats;

    public Animator dAnimator;
    void Start()
    {
        //mAttackHitbox = gameObject.AddComponent<BoxCollider2D>();
        mAttackHitbox = gameObject.GetComponent<BoxCollider2D>();
        mAttackHitbox.enabled = false;
    }

    void Update()
    {
        Inputs inputs = GameManager.Instance.GetInputs();

        if (Input.GetKeyDown((KeyCode)inputs.attack))
        {
            Attack();
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
                    //Need either a better way to do prefab loading, or a better way to check if an object should use enemy damage
                    if (collisionObject.tag == "Enemy")
                    {
                        collisionObject.GetComponent<Non_Player_Health>().Hit(enemyDamage);
                    }
                    else
                    {
                        collisionObject.GetComponent<Non_Player_Health>().Hit(stats.GetDig());
                        CinemachineShakeCam.Instance.ShakeCamera(.5f, .03f);
                    }
                }
            }
            mAttackHitbox.enabled = false;
        }
        if(mAttackHitbox.enabled == false)
        {
            dAnimator.SetBool("EndAnimation", false);
        }

    }

    public void Attack()
    {
        mAttackHitbox.enabled = true;
        dAnimator.SetBool("PlayAnimation", true);
    }
}
