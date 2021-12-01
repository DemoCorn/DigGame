using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private float health = 3.0f;
    [SerializeField] private float immunityTime = 10.0f;
    [SerializeField] private BoxCollider2D hitbox;
    private bool isVulnerable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isVulnerable)
        {
            if (IsCollidingWithEnemy())
            {
                health -= 1.0f;
                if (health <= 0.0f)
                {
                    gameObject.SetActive(false);
                }
                isVulnerable = false;
                Invoke("TurnOffImmunity", immunityTime);
            }
        }
    }

    bool IsCollidingWithEnemy()
    {
        List<Collider2D> collisions = new List<Collider2D>();

        int nCollisionCount = hitbox.OverlapCollider(new ContactFilter2D(), collisions);

        // Go through all collided objects to see if the new position would intersect with a block
        foreach (Collider2D collision in collisions)
        {
            Debug.Log("oop");
            if (collision.gameObject.tag == "Enemy")
            {
                return true;
            }
        }

        return false;
    }

    void TurnOffImmunity()
    {
        isVulnerable = true;
    }
}
