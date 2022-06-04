using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutPlatform : MonoBehaviour
{
    //private Rigidbody2D rbgd;
    // LPC: public variables do not need the SerializeField tag, however it is appropriate to have it here as I see no reason for any of these to be public variables
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
    private GameObject playerForTransform;

    void Start()
    {
        // LPC: if you never plan to use it again, don't comment a line out, delete it.
        //rbgd = GetComponent<Rigidbody2D>();
        /* LPC: initially I thought you were just being weird with not wanting to write transform.position 
                however I now understand that this is you saving the inital position. descriptive variable names are important */
        pos = transform.position;
        rend = GetComponent<Renderer>();
        // LPC: why not just initialize fallen as false?
        fallen = false;
        //rbgd.gravityScale = 0;
        // LPC: Log lines should be used to communicate important information, this is a line I'm guessing was used for debugging and should have been deleted by now
        Debug.Log("Collision1");
        BoxColl = GetComponent<BoxCollider2D>();
        // LPC: Again, if this is hard coded why is it not initialzed as the value?
        // LPC: What does avlTime even mean? when trying to figure out I discovered this isn't even used anywhere.
        AvlTime = 15f;

        xValue = pos.x;
        yValue = pos.y;
       // moveVector = new Vector3(xValue, yValue, 0);
        moveVector = new Vector3(0, yValue, 0);
        // LPC: See comments on line 37 and 43
        Colided = false;

        // LPC: gamemanager.instance.getplayertransform should be used here, if it is not a function in game manager it is easy to add and far less expensive then calling this on an object that could load multiple times
        playerForTransform = GameObject.FindGameObjectsWithTag("Player")[0];

    }

    private void ObjectMoveUp()
    {
        // LPC: this speed will be rediculous if you're not multiplying it by delta time
        transform.Translate(0, UpSpeed, 0);
    }

    private void ObjectMoveDown()
    {
        // LPC: this speed will be rediculous if you're not multiplying it by delta time
        // LPC: Why make fall speed negative? It makes more sense to just leave it up to others to put in the proper falling direction
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
        // LPC: This is not the proper way to check boolean values
        if (Colided == true)
        {
            if (TouchDisapper == false)
            {
                Invoke("BeginFalling", falldelay);
                //BeginFalling();
                //Invoke("DisableBlock", TimeToDissaper);
            }
            else if (TouchDisapper == true)
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
                playerForTransform.gameObject.transform.SetParent(gameObject.transform, true);

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

        // LPC: This is a massive waste of resources considering you already ran this function and could just use else, which you did this properly before if your commented out lines are trust worthy
        else if(CheckGrounded()==false)
        {
            playerForTransform.gameObject.transform.parent = null;
        }
    }

    // LPC: This function, litterally does nothing
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

    // LPC: This function is never used from what I can tell
    IEnumerator Fall()
    {
        Debug.Log("Collision3 FOR FALL");
        yield return new WaitForSeconds(falldelay);
        //rbgd.gravityScale = fallspeed;
        yield return 0;
    }

    // LPC: This function is never used from what I can tell
    IEnumerator waitbeforeSpawn()
    {
        yield return new WaitForSeconds(PlatformRespawnTime);
        // Debug.Log("spawning");
    }

    // LCP: why is this function named like this? You're not checking if you're grounded.
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

    // LCP: This function is the same as ObjectMoveDown
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
