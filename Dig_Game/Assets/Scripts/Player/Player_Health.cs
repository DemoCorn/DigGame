using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;


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
    public bool hasDied;
    public bool hasRetired;
    private bool revive = false;

    //player damage feedback
    public GameObject playerHurt;
    private Material matWhite;
    private Material matDefault;
    private SpriteRenderer headSR;
    private SpriteRenderer bodySR;
    private SpriteRenderer leftLegSR;
    private SpriteRenderer rightLegSR;
    public GameObject Head;
    public GameObject Body;
    public GameObject leftLeg;
    public GameObject rightLeg;

    
    private GameObject playerDamagepopup;
    private GameObject healPopup;

    private void Start()
    {
        nextPlayer = gameObject;
        startPosition = gameObject.transform.position;
        vCam = GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>();
        
        //player damage feedback
        headSR = Head.GetComponent<SpriteRenderer>();
        bodySR = Body.GetComponent<SpriteRenderer>();
        leftLegSR = leftLeg.GetComponent<SpriteRenderer>();
        rightLegSR = rightLeg.GetComponent<SpriteRenderer>();

        matWhite = Resources.Load("trans", typeof(Material)) as Material;
        
        matDefault = headSR.material;

        
        playerDamagepopup = (GameObject)Resources.Load("PlayerDamagePopup");
        healPopup = (GameObject)Resources.Load("HealPopup");
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
        ShowHeal(HealthUp.ToString());
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
                float damage = fDamage - armor;
                ShowDamage(damage.ToString());
                health -= damage < 1.0f ? 1.0f : damage;
                StartCoroutine("Hurt");


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
    void ShowDamage(string text)
    {
        if (playerDamagepopup)
        {
            GameObject prefab = Instantiate(playerDamagepopup, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }
    }
    void ShowHeal(string text)
    {
        if (healPopup)
        {
            GameObject prefab = Instantiate(healPopup, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
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
        GameManager.Instance.EndGame(hasRetired);

    }
    


    public void TestDeath()
    {
        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            Die(false);
        }
        */
        if (Input.GetKeyDown(KeyCode.R) && hasRetired == true)
        {
            GameManager.Instance.UIManager.SetRetireScreen();
        }
    }

    IEnumerator Hurt()
    {
        CinemachineShakeCam.Instance.ShakeCamera(.8f, .4f);
        headSR.material = matWhite;
        bodySR.material = matWhite;
        leftLegSR.material = matWhite;
        rightLegSR.material = matWhite;       
       // playerHurt.SetActive(true);
        yield return new WaitForSeconds(.1f);
        //playerHurt.SetActive(false);
        headSR.material = matDefault;
        bodySR.material = matDefault;
        leftLegSR.material = matDefault;
        rightLegSR.material = matDefault;
        yield return new WaitForSeconds(.1f);
        headSR.material = matWhite;
        bodySR.material = matWhite;
        leftLegSR.material = matWhite;
        rightLegSR.material = matWhite;
        yield return new WaitForSeconds(.1f);
        headSR.material = matDefault;
        bodySR.material = matDefault;
        leftLegSR.material = matDefault;
        rightLegSR.material = matDefault;
        
    }


}