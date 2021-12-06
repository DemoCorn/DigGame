using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Randomizer : MonoBehaviour
{
    public GameObject roomPrefab;
    
    private Vector2 tilePos;

    // Conditions to be met to create opening
    public static float tilePosX;
    public static float tilePosY;
    private static bool randomized = false;    


    // Start is called before the first frame update
    void Start()
    {

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
        for (float x = tilePosX - 4; x < tilePosX + 7; x++)
        {
            for (float y = tilePosY - 3; y < tilePosY + 3; y++)
            {  
                if (tilePos.x == x && tilePos.y == y)
                {
                    Destroy(gameObject);
                } 
            }
  
        }


        // Prefab Instantiating
        if (tilePos.x == tilePosX && tilePos.y == tilePosY)
        {
            Instantiate(roomPrefab, gameObject.transform.position, Quaternion.identity);
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
