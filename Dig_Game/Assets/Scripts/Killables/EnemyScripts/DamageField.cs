using UnityEngine;

public class DamageField : MonoBehaviour
{
    public bool contact = false;
    private float enemyDamage;
    private float contactDamage = 10f;
    private GameObject parent;
    

    // Start is called before the first frame update
    void Start()
    {  
        GetParent();
        enemyDamage = GetComponentInParent<EnemyStats>().damage;
        if(GetComponentInParent<EnemyStats>().damage <= 0)
        {
            enemyDamage = GetComponentInParent<EnemyProjectiles>().attackDamage;
        }
        else if(enemyDamage <= 0)
        {
            enemyDamage = contactDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Health>().TakeDamage(enemyDamage);
        }
         
    }

    private void SelfDestruct()
    {
        if (contact == true)
            return;
        Destroy(parent);
    }
    private GameObject GetParent()
    {
        parent = transform.parent.gameObject;
        return parent;
    }
}
