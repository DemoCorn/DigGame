using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedBlockSwtich : MonoBehaviour
{
    TimedBlocks Block;









    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collider2D collision)
    {




        Debug.Log("collided With Trigger");

        Block.setVisible(false);

    }


    private void Start()
    {
        enabled = true;

       Block = GameObject.FindGameObjectWithTag("Block").GetComponent<TimedBlocks>();


      //  Block.setVisible(false);








    }


    private void Update()
    {


        //Block.TimerFunction();






    }





}