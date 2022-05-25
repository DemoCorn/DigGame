using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBlocks : MonoBehaviour
{


    private Rigidbody2D rbgd;


    private Renderer rend;

    private BoxCollider2D BoxColl;

    [SerializeField] private bool Visible;

    [SerializeField] private float Timer;
    private float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        rbgd = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        BoxColl = GetComponent<BoxCollider2D>();

    }

    

    void OnCollisionEnter2D(Collision2D col)
    {



        Visible = false;








    }






    // Update is called once per frame
    void Update()
    {
        if (Visible == true)
        {
            rend.enabled = true;
            BoxColl.enabled = true;
        }


        else if(Visible==false)
        {
            rend.enabled = false;
            currentTime -= 1 * Time.deltaTime;
            BoxColl.enabled = false;

            if (currentTime==Timer)
            {
                Visible = true;
                currentTime = 0;
            }
        }






    }
}
