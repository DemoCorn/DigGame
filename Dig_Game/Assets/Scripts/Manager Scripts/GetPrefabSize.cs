using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPrefabSize : MonoBehaviour
{
    //Variables used for for looping to determine room needed to instantiate prefab into the world.
    public float lowestX;
    public float highestX;
    public float lowestY;
    public float highestY;

    [HideInInspector] public float minX;
    [HideInInspector] public float maxX;
    [HideInInspector] public float minY;
    [HideInInspector] public float maxY;

    [Header("Advanced Options")]
    public float yOffset = 0.0f;
    public float xOffset = 0.0f;
        

    void OnEnable()
    {
        Transform position = gameObject.transform;

        if (yOffset != 0.0f || xOffset != 0.0f)
        {
            OffsetFix(position);
        }

        //Save positions into our variables that we will use later
        minX = position.position.x + lowestX;
        maxX = position.position.x + highestX;
        minY = position.position.y + lowestY;
        maxY = position.position.y + highestY;

        //// DEBUGGING
        //Debug.Log("TopLeftBlock = " + topLeftBlock);
        //Debug.Log("Topleftblock X value = " + minX);
        //Debug.Log("Topleftblock Y value = " + maxY);
        //Debug.Log("BottomRightBlock = " + bottomRightBlock);
        //Debug.Log("BottomRightBlock X value = " + maxX);
        //Debug.Log("BottomRightBlock Y value = " + minY);
    }

 
    private void OffsetFix(Transform position)
    {
        position.position = new Vector3 (position.position.x + xOffset, position.position.y + yOffset, position.position.z);
        lowestX += xOffset;
        highestX += xOffset;
        lowestY += yOffset;
        highestY += yOffset;
    }
}
