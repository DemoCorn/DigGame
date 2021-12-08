using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt_Instantiate : MonoBehaviour
{
    // Attach the dirt tile objects in Unity to these Transforms
    public Transform dirt1Obj;
    public Transform dirt2Obj;
    public Transform dirt3Obj;
    public Transform mineral1Obj;


    // Start is called before the first frame update
    void Start()
    {
        // xPos will determine the amount of spaces horizontally we want to span
        // The second value in each Vector2 is the spacing vertically between tiles
        // Adjust this value according to sprite being used

        // Instantiate dirt
        for (float xPos = -30f; xPos < 30; xPos++)
        {
            // 1st layer
            Instantiate(dirt1Obj, new Vector2(xPos, 3), dirt1Obj.rotation);
            Instantiate(dirt1Obj, new Vector2(xPos, 2), dirt1Obj.rotation);
            Instantiate(dirt1Obj, new Vector2(xPos, 1), dirt1Obj.rotation);
            Instantiate(dirt1Obj, new Vector2(xPos, 0), dirt1Obj.rotation);
            Instantiate(dirt1Obj, new Vector2(xPos, -1), dirt1Obj.rotation);
            Instantiate(dirt1Obj, new Vector2(xPos, -2), dirt1Obj.rotation);
            Instantiate(dirt1Obj, new Vector2(xPos, -3), dirt1Obj.rotation);
            Instantiate(dirt1Obj, new Vector2(xPos, -4), dirt1Obj.rotation);

            // 2nd layer
            Instantiate(dirt2Obj, new Vector2(xPos, -5), dirt2Obj.rotation);
            Instantiate(dirt2Obj, new Vector2(xPos, -6), dirt2Obj.rotation);
            Instantiate(dirt2Obj, new Vector2(xPos, -7), dirt2Obj.rotation);
            Instantiate(dirt2Obj, new Vector2(xPos, -8), dirt2Obj.rotation);
            Instantiate(dirt2Obj, new Vector2(xPos, -9), dirt2Obj.rotation);
            Instantiate(dirt2Obj, new Vector2(xPos, -10), dirt2Obj.rotation);
            Instantiate(dirt2Obj, new Vector2(xPos, -11), dirt2Obj.rotation);
            Instantiate(dirt2Obj, new Vector2(xPos, -12), dirt2Obj.rotation);

            // 3rd layer
            Instantiate(dirt3Obj, new Vector2(xPos, -13), dirt3Obj.rotation);
            Instantiate(dirt3Obj, new Vector2(xPos, -14), dirt3Obj.rotation);
            Instantiate(dirt3Obj, new Vector2(xPos, -15), dirt3Obj.rotation);
            Instantiate(dirt3Obj, new Vector2(xPos, -16), dirt3Obj.rotation);
            Instantiate(dirt3Obj, new Vector2(xPos, -17), dirt3Obj.rotation);
            Instantiate(dirt3Obj, new Vector2(xPos, -18), dirt3Obj.rotation);
            Instantiate(dirt3Obj, new Vector2(xPos, -19), dirt3Obj.rotation);
            Instantiate(dirt3Obj, new Vector2(xPos, -20), dirt3Obj.rotation);

        }

        // Random Minerals

        for (int x = 0; x < 10; x++)
        {
            Instantiate(mineral1Obj, new Vector2(Random.Range(-30, 30), Random.Range(3, -10)), mineral1Obj.rotation);
        }

    }


        // Update is called once per frame
        void Update()
        {

        }

    
}