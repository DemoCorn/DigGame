using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt_Instantiate : MonoBehaviour
{
    // Attach the dirt tile objects in Unity to these Transforms
    [SerializeField] GameObject dirt1Obj;
    [SerializeField] GameObject dirt2Obj;
    [SerializeField] GameObject dirt3Obj;
    [SerializeField] GameObject mineral1Obj;

    [SerializeField] GameObject roomPrefabSmall;
    [SerializeField] GameObject roomPrefabMedium;
    [SerializeField] GameObject roomPrefabLarge;

    private static int randomRoomSize;
    private static bool randomized = false;

    public static float prefabPosX;
    public static float prefabPosY;

    // Start is called before the first frame update
    void Start()
    {
        // xPos will determine the amount of spaces horizontally we want to span
        // The second value in each Vector2 is the spacing vertically between tiles
        // Adjust this value according to sprite being used

        // Changed to 2 and 2 from 1 to 3 to avoid crashes in MVP
        randomRoomSize = (int)Random.Range(1.0f, 2.999f);
        Debug.Log(randomRoomSize);
        // Call randomize once if it hasn't already been done
        if (!randomized)
        {
            Randomize();
        }

        // Instantiate dirt
        Transform blockTransform;
        GameObject block;

        for (float xPos = -30f; xPos < 30; xPos++)
        {
            for (float yPos = -20; yPos <= 3; yPos++)
            {
                if (yPos <= -13)
                {
                    blockTransform = dirt3Obj.transform;
                    block = dirt3Obj;
                }
                else if (yPos <= -5)
                {
                    blockTransform = dirt2Obj.transform;
                    block = dirt2Obj;
                }
                else
                {
                    blockTransform = dirt1Obj.transform;
                    block = dirt1Obj;
                }
                block = Instantiate(block, new Vector2(xPos, yPos), blockTransform.rotation);
                
                switch (randomRoomSize)
                {
                    case 1:
                        if ((xPos >= prefabPosX - 6 && yPos >= prefabPosY - 3) && (xPos < prefabPosX + 6 && yPos < prefabPosY + 3))
                        {
                            Destroy(block);
                        }
                        break;

                    case 2:
                        if ((xPos >= prefabPosX - 7 && yPos >= prefabPosY - 6) && (xPos < prefabPosX + 7 && yPos < prefabPosY + 6))
                        {
                            Destroy(block);
                        }
                        break;

                    case 3:
                        if ((xPos >= prefabPosX - 9 && yPos >= prefabPosY - 8) && (xPos < prefabPosX + 9 && yPos < prefabPosY + 8))
                        {
                            Destroy(block);
                        }
                        break;
                }
            }
        }

        // Prefab Instantiating
        switch (randomRoomSize)
        {
            case 1:
                Instantiate(roomPrefabSmall, new Vector3(prefabPosX, prefabPosY, 0) - new Vector3(1, 0, 0), Quaternion.identity);
                break;
            case 2:
                //Instantiate(roomPrefabMedium, new Vector3(prefabPosX, prefabPosY, 0), Quaternion.identity);
                Instantiate(roomPrefabMedium, new Vector3(prefabPosX, prefabPosY, 0) - new Vector3(0, 1, 0), Quaternion.identity);
                break;
            case 3:
                Instantiate(roomPrefabLarge, new Vector3(prefabPosX, prefabPosY, 0), Quaternion.identity);
                break;
        }

        int mineralX;
        int mineralY;
        // Random Minerals
        for (int x = 0; x < 10; x++)
        {
            mineralX = Random.Range(-30, 30);
            mineralY = Random.Range(3, -10);
            block = Instantiate(mineral1Obj, new Vector2(mineralX, mineralY), mineral1Obj.transform.rotation);

            switch (randomRoomSize)
            {
                case 1:
                    if ((mineralX >= prefabPosX - 6 && mineralY >= prefabPosY - 3) && (mineralX < prefabPosX + 6 && mineralY < prefabPosY + 3))
                    {
                        Destroy(block);
                    }
                    break;

                case 2:
                    if ((mineralX >= prefabPosX - 7 && mineralY >= prefabPosY - 6) && (mineralX < prefabPosX + 7 && mineralY < prefabPosY + 6))
                    {
                        Destroy(block);
                    }
                    break;

                case 3:
                    if ((mineralX >= prefabPosX - 9 && mineralY >= prefabPosY - 8) && (mineralX < prefabPosX + 9 && mineralY < prefabPosY + 8))
                    {
                        Destroy(block);
                    }
                    break;
            }
        }

    }

    public void Randomize()
    {
        // Randomizing chosen condition to create opening for both X & Y values of attached tile
        prefabPosX = Random.Range(-22, 22);
        prefabPosY = Random.Range(-3, -10);
        randomized = true;
    }

}