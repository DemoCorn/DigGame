using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Randomizer : MonoBehaviour
{
    public GameObject roomPrefabSmall;
    public GameObject roomPrefabMedium;
    public GameObject roomPrefabLarge;

    private Vector2 tilePos;

    // Conditions to be met to create opening
    public static float tilePosX;
    public static float tilePosY;
    private static int randomRoomSize;
    private static bool randomized = false;    


    // Start is called before the first frame update
    void Start()
    {
        randomRoomSize = Random.Range(1, 3);
        // Call randomize once if it hasn't already been done
        if (!randomized)
        {
            Randomize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        tilePos = GetComponent<Transform>().position;

        // Remove existing tiles before inserting prefab
        switch (randomRoomSize)
        {
            case 1:
                for (float x = tilePosX - 6; x < tilePosX + 6; x++)
                {
                    for (float y = tilePosY - 3; y < tilePosY + 3; y++)
                    {
                        if (tilePos.x == x && tilePos.y == y)
                        {
                            Destroy(gameObject);
                        }
                    }

                }
                break;
            case 2:
                for (float x = tilePosX - 7; x < tilePosX + 7; x++)
                {
                    for (float y = tilePosY - 6; y < tilePosY + 6; y++)
                    {
                        if (tilePos.x == x && tilePos.y == y)
                        {
                            Destroy(gameObject);
                        }
                    }

                }
                break;
            case 3:
                for (float x = tilePosX - 9; x < tilePosX + 9; x++)
                {
                    for (float y = tilePosY - 8; y < tilePosY + 8; y++)
                    {
                        if (tilePos.x == x && tilePos.y == y)
                        {
                            Destroy(gameObject);
                        }
                    }

                }
                break;

        }

        
 


        // Prefab Instantiating
        if (tilePos.x == tilePosX && tilePos.y == tilePosY)
        {

            switch (randomRoomSize)
            {
                case 1:
                    Instantiate(roomPrefabSmall, gameObject.transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(roomPrefabSmall, gameObject.transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(roomPrefabSmall, gameObject.transform.position, Quaternion.identity);
                    break;
            }
            
        }

    }

    public void Randomize()
    {
        // Randomizing chosen condition to create opening for both X & Y values of attached tile
        tilePosX = Random.Range(-22, 22);
        tilePosY = Random.Range(-1, -3);
        randomized = true;
    }

}
