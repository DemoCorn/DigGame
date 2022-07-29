using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon_Attack : MonoBehaviour
{
    [SerializeField] Player_WeaponStats stats;
    [SerializeField] private GameObject trailRenderer;
    Animator mAnimator;
    Collider2D mHitbox;

    

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
        mHitbox = GetComponent<Collider2D>();
        mHitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs inputs = GameManager.Instance.GetInputs();

        if (Input.GetKeyDown((KeyCode) inputs.attack))
        {
            Attack();
        }
        if (mAnimator.GetBool("WeaponAttackDone"))
        {
            mAnimator.SetBool("WeaponAttackDone", false);
            mHitbox.enabled = false;
            trailRenderer.SetActive(false);
        }

        // Check if attack hitbox is active to skip some execution if it's not
        if (mHitbox.enabled)
        {
            List<Collider2D> collisions = new List<Collider2D>();

            int nCollisionCount = mHitbox.OverlapCollider(new ContactFilter2D(), collisions);
            GameObject collisionObject;

            // Go through all collided objects to damage blocks
            foreach (Collider2D collision in collisions)
            {
                collisionObject = collision.gameObject;
                if (collisionObject.GetComponent<Non_Player_Health>() != null && collisionObject.tag == "Enemy")
                {
                    collisionObject.GetComponent<Non_Player_Health>().Hit(stats.GetAttack());
                    CinemachineShakeCam.Instance.ShakeCamera(.8f, .05f);
                }
            }
        }
    }

    public void Attack()
    {
        mHitbox.enabled = true;
        mAnimator.SetInteger("Class", (int)GameManager.Instance.InventoryManager.getPlayerClass());
        mAnimator.SetBool("PlayAttack", true);
        trailRenderer.SetActive(true);
    }
}
