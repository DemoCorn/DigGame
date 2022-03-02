using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemEnemyScript : MonoBehaviour
{
    public float health =100;
    
    //Do damage
    public int Damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    //do damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("sada");
            collision.gameObject.GetComponent<TempPlayer>().TakeDamage(Damage);
            
        }
    }
   public void TakeDamage(float Damage)
    {
        health-=Damage;
    }
    
   
}
