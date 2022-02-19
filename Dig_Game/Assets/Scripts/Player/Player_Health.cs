using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private float health = 3.0f;
    [SerializeField] private float maxHealth = 3.0f;
    [SerializeField] private float armor = 0.0f;
    [SerializeField] private float immunityTime = 10.0f;
    [SerializeField] private BoxCollider2D hitbox;
    private bool isVulnerable = true;

    // Update is called once per frame
    void Update()
    {
        if (isVulnerable)
        {
            float nDamageTaken = IsCollidingWithEnemy();
            if (nDamageTaken > 0.0f)
            {
                health -= nDamageTaken - armor;

                if (health <= 0.0f)
                {
                    GameManager.Instance.EndGame(false);
                }

                isVulnerable = false;
                Invoke("TurnOffImmunity", immunityTime);
            }
        }
        
    }

    private float IsCollidingWithEnemy()
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

    private void TurnOffImmunity()
    {
        isVulnerable = true;
    }

    public float GetHealth()
    {
        return health;
    }

    public void Equip(float healthChange, float armorChange)
    {
        maxHealth += healthChange;
        armor += armorChange;
    }
  
}