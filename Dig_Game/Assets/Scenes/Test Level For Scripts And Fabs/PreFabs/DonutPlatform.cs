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
    private BoxCollider2D BoxColl;

    void Start()
    {
        rbgd = GetComponent<Rigidbody2D>();
        pos = transform.position;
        rend = GetComponent<Renderer>();
        fallen = false;
        rbgd.gravityScale = 0;
        Debug.Log("Collision1");
        BoxColl = GetComponent<BoxCollider2D>();
    }

    private void ObjectMoveUp()
    {
        transform.Translate(0, UpSpeed, 0);
    }

    private void SpawnNewPlatform()
    {
        if (fallen == true)
        {
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
        if (CheckGrounded())
        {
            Debug.Log("Collision");
            //StartCoroutine("Fall", falldelay);
            ObjectMoveUp();
            //DisableBlock();
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
        rbgd.gravityScale = fallspeed;
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
        RaycastHit2D hit = Physics2D.BoxCast(BoxColl.bounds.center, BoxColl.bounds.size, 0f, Vector2.up, 0.5f);
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
    }

    private void InstantFall()
    {
        transform.Translate(0, -fallspeed, 0);
    }
}
