using UnityEngine;

public class DamageField : MonoBehaviour
{
    private float enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        enemyDamage = GetComponentInParent<FlyingEnemy>().attackDamage;
        //Debug.Log(enemyDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          TestMovement player = collision.GetComponent<TestMovement>();
          if(player != null)
            {
                player.TakeDamage(enemyDamage);
            }
        }
    }
}
