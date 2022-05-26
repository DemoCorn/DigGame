using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutPlatform : MonoBehaviour
{
    //private Rigidbody2D rbgd;
    [SerializeField] public float falldelay;
    private Vector2 pos;
    [SerializeField] public float PlatformRespawnTime;
    private Renderer rend;
    private bool fallen;
    [SerializeField] public float fallspeed;
    [SerializeField] public float UpSpeed;
    private BoxCollider2D BoxColl;
    private float Currenttime;
    private float AvlTime;
    [SerializeField] public float TimeToDissaper;
    [SerializeField] public bool TouchDisapper;

    private float yValue;
    private float xValue;
    private float rayDistance;
    private Vector3 moveVector;
    private bool Colided;

    void Start()
    {
        //rbgd = GetComponent<Rigidbody2D>();
        pos = transform.position;
        rend = GetComponent<Renderer>();
        fallen = false;
        //rbgd.gravityScale = 0;
        Debug.Log("Collision1");
        BoxColl = GetComponent<BoxCollider2D>();
        AvlTime = 15f;

        xValue = pos.x;
        yValue = pos.y;
       // moveVector = new Vector3(xValue, yValue, 0);
        moveVector = new Vector3(0, yValue, 0);
        Colided = false;

    }

    private void ObjectMoveUp()
    {
        transform.Translate(0, UpSpeed, 0);
    }

    private void ObjectMoveDown()
    {
        transform.Translate(0, -fallspeed, 0);
    }

    private void SpawnNewPlatform()
    {
        //if (fallen == true)
        {
             Debug.Log("Spawned");
            transform.position = pos;
            rend.enabled = true;
            fallen = false;
            // Debug.Log(pos);
            GetComponent<Collider2D>().enabled = true;
           // rbgd.gravityScale = 0;
        }
    }

    private void Update()
    {
        if (Colided == true)
        {
            if (TouchDisapper == false)
            {
                Invoke("BeginFalling", falldelay);
                //BeginFalling();
                //Invoke("DisableBlock", TimeToDissaper);
            }
            else if (TouchDisapper==true)
            {
                Invoke("DisableBlock", falldelay);
            }
        }
        //  Debug.Log(pos);
        if (CheckGrounded())
        {

            if (Colided == false)
            {
                Debug.Log("Collision");
                //StartCoroutine("Fall", falldelay);
                //ObjectMoveUp();
                //DisableBlock();
                //ObjectMoveDown();

                Colided = true;
            }
        }
        //else
        //{
        //    GetComponent<Collider2D>().enabled = false;
        //    Debug.Log("destroy");
        //    rend.enabled = false;
        //    rbgd.gravityScale = 0;
        //    fallen = true;
        //    Invoke("SpawnNewPlatform", PlatformRespawnTime);
        //}
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision2");
        //  Debug.Log("disabled");
        //if (col.collider.CompareTag("Player") == true)
        //{
        //    Debug.Log("Collision");
        //    Invoke("Fall", falldelay);
        //}
    }

    IEnumerator Fall()
    {
        Debug.Log("Collision3 FOR FALL");
        yield return new WaitForSeconds(falldelay);
        //rbgd.gravityScale = fallspeed;
        yield return 0;
    }

    IEnumerator waitbeforeSpawn()
    {
        yield return new WaitForSeconds(PlatformRespawnTime);
        // Debug.Log("spawning");
    }


    private bool CheckGrounded()
    {
        Debug.Log("ground check!");
        RaycastHit2D hit = Physics2D.BoxCast(BoxColl.bounds.center, BoxColl.bounds.size, 0f, Vector2.up, 0.2f);
        //if (!hit)
        //    Debug.Log("HIT none");
        //Debug.Log(hit.transform.name);
        return hit;
    }



    private void DisableBlock()
    {
        Debug.Log("Block Disabled");
        rend.enabled = false;
        BoxColl.enabled = false;
        Colided = false;

        Invoke("SpawnNewPlatform", PlatformRespawnTime);
    }

    private void InstantFall()
    {
        transform.Translate(0, -fallspeed, 0);
    }

    public void BeginFalling()
    {
        Debug.Log("BeginFalling() Activated!");
        transform.Translate(moveVector * -fallspeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit Detected to block");

        DisableBlock();
    }

}
