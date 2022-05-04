using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutPlatform : MonoBehaviour
{


    private Rigidbody2D rbgd;

    [SerializeField] public float falldelay;

    private Vector2 pos;

 
    [SerializeField] public float PlatformRespawnTime;

    private Renderer rend;

    private bool fallen;

    [SerializeField] public float fallspeed;
    [SerializeField] public float UpSpeed;








    void Start()
    {
        rbgd = GetComponent<Rigidbody2D>();
        pos = transform.position;
        rend = GetComponent<Renderer>();
        fallen = false;

        rbgd.gravityScale = fallspeed;
    }




     private void ObjectMoveUp()
    {
        
        transform.Translate(0, UpSpeed, 0);
    }
        







    private void SpawnNewPlatform()
    {

        if (fallen == true)
        {


            StartCoroutine(waitbeforeSpawn());


          //  Debug.Log("Spawned");


            transform.position = pos;

            rend.enabled = true;

            fallen = false;

           // Debug.Log(pos);

            GetComponent<Collider2D>().enabled = true;

            rbgd.gravityScale = 0;
        }
    }


    private void Update()
    {

        //  Debug.Log(pos);

        if (fallspeed < 0)
        {
        //    ObjectMoveUp();
        }
    }




    




    void OnCollisionEnter2D(Collision2D col)
    {
      //  Debug.Log("Collision2");


      //  Debug.Log("disabled");
        GetComponent<Collider2D>().enabled = false;


     //   Debug.Log("destroy");
        rend.enabled = false;


        if (col.collider.CompareTag("Player")==true)
        {
            Debug.Log("Collision");
            StartCoroutine(Fall());


        }

        fallen = true;



        SpawnNewPlatform();

    }


    IEnumerator Fall()
    {

      //  Debug.Log("Collision3");
        yield return new WaitForSeconds(falldelay);

        rbgd.gravityScale = fallspeed;

        yield return 0;

    }

    IEnumerator waitbeforeSpawn()
    {
        yield return new WaitForSeconds(PlatformRespawnTime);
       // Debug.Log("spawning");
        
    }



}
