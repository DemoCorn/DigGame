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

    [SerializeField] private float attackRateWhenHolding = 0.1f;
    private float nextAttackTime = 0;

    [SerializeField] private AudioSource attackSFX;
    private bool soundBool = false;
    

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
        mHitbox = GetComponent<Collider2D>();
        attackSFX = GetComponentInChildren<AudioSource>();
        mHitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs inputs = GameManager.Instance.GetInputs();

        if (Input.GetKeyDown((KeyCode) inputs.attack))
        {
            Attack();
            soundBool = true;
        }
        if (Time.time > nextAttackTime && Input.GetKey((KeyCode)inputs.attack))
        {
            Attack();
            nextAttackTime = Time.time + attackRateWhenHolding;
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

            if (soundBool)
            {
                attackSFX.Play();
                soundBool = false;
            }
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
