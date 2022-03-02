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

    public void TakeDamage(float fDamage)
    {
        if (isVulnerable)
        {
            if (fDamage > 0.0f)
            {
                health -= fDamage - armor;

                if (health <= 0.0f)
                {
                    Die();
                    GameManager.Instance.EndGame(false);
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

    public void Die()
    {
        // Make player lose Inventory and Equipment
        FindObjectOfType<Inventory_Manager>().DieReset();
        // Make a new player
        GameObject newPlayer = (GameObject)Instantiate(nextPlayer, startPosition, Quaternion.identity);
        // Set to a new random class
        FindObjectOfType<Inventory_Manager>().RandomizeClass();
        // Point Camera to newPlayer
        vCam.LookAt = nextPlayer.transform;
        vCam.Follow = nextPlayer.transform;
        // Destroy old player
        Destroy(gameObject);
        
    }

    public void Retire()
    {
        // Make a new player
        GameObject newPlayer = (GameObject)Instantiate(nextPlayer, startPosition, Quaternion.identity);
        // Set to a new random class
        FindObjectOfType<Inventory_Manager>().RandomizeClass();
        // Point Camera to newPlayer
        vCam.LookAt = newPlayer.transform;
        vCam.Follow = newPlayer.transform;
        // Destroy old player
        Destroy(gameObject);
    }
  
}