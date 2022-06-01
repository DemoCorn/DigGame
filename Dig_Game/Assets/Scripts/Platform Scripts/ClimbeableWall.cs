using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbeableWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }



    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;
    bool grounded;
    bool facingRight;
    Rigidbody2D rb2d;
    bool wallsliding;

    public void CheckGrounded(bool isgrounded)
    {
        if (!isgrounded)
        {

            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);


            if (facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
            {

                if(wallCheck)
                {
                    HandleWallSliding();
                }


            }


        }
    }



    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f);



        wallsliding = true;


        if(Input.GetButtonDown("Jump"))
        {
            if(facingRight)
            {
                //rb2d.AddForce(new Vector2(-1,3)*jumpPower)
            }
        }




    }    




    // Update is called once per frame
    void Update()
    {
        
    }
}
