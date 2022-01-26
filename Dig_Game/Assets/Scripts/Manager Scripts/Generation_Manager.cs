using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation_Manager : MonoBehaviour
{
    // Attach the dirt tile objects in Unity to these Transforms
    public GameObject dirtObj;
    public GameObject mineral1Obj;

    public GameObject[] roomPrefabs; 

    private static int randomRoom;
    private static bool randomized = false;

    public static float prefabPosX;
    public static float prefabPosY;

    public GetPrefabSize _prefabSizeScript;

    // Start is called before the first frame update
    void Start()
    {
        // xPos will determine the amount of spaces horizontally we want to span
        // The second value in each Vector2 is the spacing vertically between tiles
        // Adjust this value according to sprite being used

        // Call randomize once if it hasn't already been done
        if (!randomized)
        {
            Randomize();
        }
        Debug.Log("Generated prefab: " + randomRoom + " from list");


        // Instantiate dirt
        GameObject block;

        // Instantiate roomPrefab and store script min and max values
        Instantiate(roomPrefabs[randomRoom], new Vector3(prefabPosX, prefabPosY, 0), Quaternion.identity);
        _prefabSizeScript = FindObjectOfType<GetPrefabSize>();        

        // Instantiate Dirt Blocks
        for (float xPos = -30f; xPos < 30; xPos++)
        {
            for (float yPos = -20; yPos <= 3; yPos++)
            {
                block = Instantiate(dirtObj, new Vector2(xPos, yPos), dirtObj.transform.rotation);            

                // Destroy dirt blocks based on prefab size
                if ((xPos >= _prefabSizeScript.minX && xPos <= _prefabSizeScript.maxX)
                    && (yPos >= _prefabSizeScript.minY && yPos <= _prefabSizeScript.maxY))
                {
                    Destroy(block);
                }
            }
        }


        int mineralX;
        int mineralY;
        // Random Minerals
        for (int x = 0; x < 10; x++)
        {
            mineralX = Random.Range(-30, 30);
            mineralY = Random.Range(3, -10);
            block = Instantiate(mineral1Obj, new Vector2(mineralX, mineralY), mineral1Obj.transform.rotation);

            // Destroy dirt blocks based on prefab size
            if ((mineralX >= _prefabSizeScript.minX && mineralX <= _prefabSizeScript.maxX)
                && (mineralY >= _prefabSizeScript.minY && mineralY <= _prefabSizeScript.maxY))
            {
                Destroy(block);
            }
        }

    }

    public void Randomize()
    {
        // Randomizing chosen condition to create opening for both X & Y values of attached tile
        prefabPosX = Random.Range(-22, 22);
        prefabPosY = Random.Range(-3, -10);
        randomized = true;

        //Randomize which room out of the array to select
        // Note - passing min and max INT values into the range overload the function to a (minINCLUSIVE, maxEXCLUSIVE) range.
        // Note - passing min and max FLOAT values into the range overload the function to a (minINCLUSIVE, maxINCLUSIVE) range.
        randomRoom = Random.Range(0, roomPrefabs.Length);
    }

}