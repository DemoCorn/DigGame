using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPrefabSize : MonoBehaviour
{
    //Variables used for for looping to determine room needed to instantiate prefab into the world.
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private GameObject topLeftBlock;
    private GameObject bottomRightBlock;
        

    void OnEnable()
    {
        //Find the corresponding TopLeft and BottomRight blocks through the use of tagging them in Unity first.
        topLeftBlock = GameObject.FindWithTag("TopLeftBlock");
        bottomRightBlock = GameObject.FindWithTag("BottomRightBlock");   

        //Save positions into our variables that we will use later
        minX = topLeftBlock.transform.position.x;
        maxY = topLeftBlock.transform.position.y;
        maxX = bottomRightBlock.transform.position.x;
        minY = bottomRightBlock.transform.position.y;
        
        
        //// DEBUGGING
        //Debug.Log("TopLeftBlock = " + topLeftBlock);
        //Debug.Log("Topleftblock X value = " + minX);
        //Debug.Log("Topleftblock Y value = " + maxY);
        //Debug.Log("BottomRightBlock = " + bottomRightBlock);
        //Debug.Log("BottomRightBlock X value = " + maxX);
        //Debug.Log("BottomRightBlock Y value = " + minY);
    }

 
    void Update()
    {

    }
}
