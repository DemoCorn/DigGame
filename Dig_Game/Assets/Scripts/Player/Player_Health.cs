using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private float health = 3.0f;
    [SerializeField] private float maxHealth = 3.0f;
    [SerializeField] private float armor = 0.0f;
    [SerializeField] private float immunityTime = 10.0f;
    [SerializeField] private BoxCollider2D hitbox;
    Vector3 startPosition;
    GameObject nextPlayer;
    CinemachineVirtualCamera vCam;
    private bool isVulnerable = true;
    bool hasDied;
    bool hasRetired;
    private bool revive = false;

    private void Start()
    {
        nextPlayer = gameObject;
        startPosition = gameObject.transform.position;
        vCam = GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (isVulnerable)
        {
            float nDamageTaken = IsCollidingWithEnemy();
            TakeDamage(nDamageTaken);
        }

        TestDeath();

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

    public void Heal(float HealthUp)
    {
        health += HealthUp;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetDefence()
    {
        return armor;
    }

    public void SetRevive(bool rev)
    {
        revive = rev;
    }

    public void TakeDamage(float fDamage)
    {
        if (isVulnerable)
        {
            if (fDamage > 0.0f)
            {
                health -= fDamage - armor;

                if (health <= 0.0f)
                {
                    Debug.Log(health);
                    Die(false);
                    //GameManager.Instance.EndGame(false);
                }

                isVulnerable = false;
                Invoke("TurnOffImmunity", immunityTime);
            }
        }
    }

    public void Equip(float healthChange, float armorChange)
    {
        maxHealth += healthChange;
        armor += armorChange;
    }

    public void Die(bool hasRetired)
    {
        if (!hasRetired)
        {
            if (revive)
            {
                health = maxHealth / 2;
                return;
            }
            // Make player lose Inventory and Equipment
            GameManager.Instance.InventoryManager.DieReset();         
        }

        // Reset Health
        health = maxHealth;

        // Set to a new random class
        GameManager.Instance.InventoryManager.RandomizeClass();
        
        // Reset / Respawn player
        transform.position = startPosition;

    }

    public void TestDeath()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {        
            Die(false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die(true);
        }
    }
      
}