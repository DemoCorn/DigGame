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
            float nDamageTaken = IsCollidingWithEnemy();
            if (nDamageTaken > 0.0f)
            {
                health -= nDamageTaken;
                if (health <= 0.0f)
                {
                    gameObject.SetActive(false);
                }
                isVulnerable = false;
                Invoke("TurnOffImmunity", immunityTime);
            }
        }
    }

    float IsCollidingWithEnemy()
    {
        List<Collider2D> collisions = new List<Collider2D>();

        int nCollisionCount = hitbox.OverlapCollider(new ContactFilter2D(), collisions);

        // Go through all collided objects to see if the position would intersect with an enemy
        foreach (Collider2D collision in collisions)
        {
            if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy_Damage>() != null)
            {
                Debug.Log("Player Hit");
                return collision.gameObject.GetComponent<Enemy_Damage>().damage;
            }
        }

        return 0.0f;
    }

    void TurnOffImmunity()
    {
        isVulnerable = true;
    }

    public float GetHealth()
    {
        return health;
    }
}
