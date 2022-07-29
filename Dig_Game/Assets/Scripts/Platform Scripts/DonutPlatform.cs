using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutPlatform : MonoBehaviour
{
    [SerializeField] float falldelay;
    //this Variable is for the storing the initial postion of the block
    private Vector2 pos;
    [SerializeField] float PlatformRespawnTime;
    private Renderer rend;
    private bool fallen=false;
    [SerializeField] float fallspeed;
    [SerializeField] float UpSpeed;
    private BoxCollider2D BoxColl;
    private float Currenttime;
    [SerializeField] float TimeToDissaper;
    [SerializeField]  bool TouchDisapper;

    private float yValue;
    private float xValue;
    private float rayDistance;
    private Vector3 moveVector;
    private bool Colided=false;
    private GameObject playerForTransform;
    private int counter = 0;
    bool isgroundedcollect;
    Player_Movement movementofplayer;
    

    void Start()
    {
        pos = transform.position;
        rend = GetComponent<Renderer>();
        BoxColl = GetComponent<BoxCollider2D>();
        xValue = pos.x;
        yValue = pos.y;
        moveVector = new Vector3(0, yValue, 0);

        playerForTransform = GameManager.Instance.GetPlayerTransform();

        movementofplayer = GameObject.Find("Player 2").GetComponent<Player_Movement>();
        isgroundedcollect = movementofplayer.getGrounded();
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
            // Debug.Log("Spawned");
            transform.position = pos;
            rend.enabled = true;
            fallen = false;
            // Debug.Log(pos);
            GetComponent<Collider2D>().enabled = true;
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
            }
            else if (TouchDisapper == true)
            {
                Invoke("DisableBlock", falldelay);
            }
        }
        //  Debug.Log(pos);
        if (CheckGrounded())
        {
            //Debug.Log("CheckGrounded: " + CheckGrounded());
           
            if (Colided == false)
            {
                playerForTransform.gameObject.transform.SetParent(gameObject.transform, true);
                //Debug.Log("chECKGROUNDED playertransform set to platform");
                Colided = true;
            }
        }
        else
        {
            
            //Debug.Log("CheckGrounded: " + CheckGrounded());
            playerForTransform.gameObject.transform.parent = null;
        }
    }

    // LPC: This function, litterally does nothing
    //void OnCollisionEnter2D(Collision2D col)
    //{
        
    //    //  Debug.Log("disabled");
    //    //if (col.collider.CompareTag("Player") == true)
    //    //{
    //    //    Debug.Log("Collision");
    //    //    Invoke("Fall", falldelay);
    //    //}
    //}


    //Function checks for Collision with the block and the player 
    private bool CheckGrounded()
    {
      //  Debug.Log("ground check!");
        RaycastHit2D hit = Physics2D.BoxCast(BoxColl.bounds.center, BoxColl.bounds.size, 0f, Vector2.up, 0.2f);
        //if (!hit)
        //    Debug.Log("HIT none");
        //Debug.Log(hit.transform.name);       
        return hit;
    }

    private void DisableBlock()
    {
       // Debug.Log("Block Disabled");
        rend.enabled = false;
        BoxColl.enabled = false;
        Colided = false;

        Invoke("SpawnNewPlatform", PlatformRespawnTime);
        playerForTransform.gameObject.transform.parent = null;
        counter = 0;
    }

    public void BeginFalling()
    {
      //  Debug.Log("BeginFalling() Activated!");
        transform.Translate(moveVector * -fallspeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         Debug.Log("Hit Detected to block");

        DisableBlock();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


    }

}
