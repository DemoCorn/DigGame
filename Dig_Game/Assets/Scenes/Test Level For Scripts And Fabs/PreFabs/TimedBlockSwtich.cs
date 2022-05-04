using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedBlockSwtich : MonoBehaviour
{
    TimedBlocks Block;









    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Block = GameObject.FindGameObjectWithTag("InvisibleBlock").GetComponent<TimedBlocks>();


        Block.isvisible = false;

    }


    private void Start()
    {

       
        Block = GameObject.FindGameObjectWithTag("Block").GetComponent<TimedBlocks>();


        Block.setVisible(false);








    }



}