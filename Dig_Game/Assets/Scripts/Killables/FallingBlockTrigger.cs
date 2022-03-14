using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockTrigger : MonoBehaviour
{
    public GameObject[] fallingObjects;
    public bool switc = true;

    private void OnDisable()
    {
        foreach (GameObject i in fallingObjects)
        {
            i.GetComponent<FallingBlock>().objectTrigger = true;
        }
    }
    /*
    private void Update()
    {
        if(switc == true) 
        {
            switc = false;
            foreach (GameObject i in fallingObjects)
            {
                i.GetComponent<FallingBlock>().objectTrigger = true;
            }
        }
        
    }
    */
}
