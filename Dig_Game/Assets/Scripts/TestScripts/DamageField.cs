using UnityEngine;

public class DamageField : MonoBehaviour
{
    public bool contact = false;
    private float enemyDamage;
    private GameObject parent;
    

    // Start is called before the first frame update
    void Start()
    {
        
        GetParent();
        enemyDamage = GetComponentInParent<EnemyAI>().attackDamage;
        if(GetComponentInParent<EnemyAI>().attackDamage <= 0)
        {
            enemyDamage = GetComponentInParent<EnemyProjectiles>().attackDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
          TestMovement player = collision.GetComponent<TestMovement>();
          if(player != null)
            {
                player.TakeDamage(enemyDamage);
                SelfDestruct();
            }
        }
        
        SelfDestruct();        
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
