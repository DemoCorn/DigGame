using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedBlocks : MonoBehaviour
{



    private Renderer rend;

    [SerializeField] public bool isvisible;
   
    





    // Start is called before the first frame update
    void Start()
    {


        rend = GetComponent<Renderer>();

        isvisible = true;

       





    }

    public void setVisible(bool val)
    {

        if(val==true)
        {
            isvisible = true;
        }

        else
        {
            isvisible = false;
        }
    }


    // Update is called once per frame
    void Update()
    {


        if (isvisible == false)
        {
            InVisibleNONInteractable();
        }

        else
        {
            {
                VisibleInteractable();
            }   
        }







    }



    void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<Collider2D>().enabled = false;
        rend.enabled = false;










    }



    public void VisibleInteractable()
    {
        rend.enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    public void InVisibleNONInteractable()
    {
        Debug.Log("activated");
       

        rend.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }





}
