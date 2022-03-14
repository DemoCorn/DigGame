using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public Vector3 moveDir;

    public int Damage = 20;

 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, moveDir);
        StartCoroutine(DestoryShot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Non_Player_Health>().Hit(Damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
       
    }

    IEnumerator DestoryShot()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(gameObject);
    }
}
