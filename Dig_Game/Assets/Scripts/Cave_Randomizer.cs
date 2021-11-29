using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave_Randomizer : MonoBehaviour
{
    // Attach this script to parent Tile class for all tiles to inherit.

    // Actual tile position this script is attached to
    public Vector2 tilePos;

    // Conditions to be met to create opening
    public static float tilePosX;
    public static float tilePosY;
    private static bool randomized = false;
    private static bool isTile;


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
        
        // Middle
        if (tilePos.x == tilePosX && tilePos.y == tilePosY)
        {
            Destroy(gameObject);
        }
        // Bottom
        if (tilePos.x == tilePosX - 1 && tilePos.y == tilePosY)
        {
            Destroy(gameObject);
        }
        // Top
        if (tilePos.x == tilePosX + 1 && tilePos.y == tilePosY)
        {
            Destroy(gameObject);
        }
        // Right
        if (tilePos.x == tilePosX && tilePos.y == tilePosY + 1)
        {
            Destroy(gameObject);
        }
        // Left
        if (tilePos.x == tilePosX && tilePos.y == tilePosY - 1)
        {
            Destroy(gameObject);
        }
        // TopLeft
        if (tilePos.x == tilePosX -1 && tilePos.y == tilePosY + 1)
        {
            Destroy(gameObject);
        }
        // TopRight
        if (tilePos.x == tilePosX + 1 && tilePos.y == tilePosY + 1)
        {
            Destroy(gameObject);
        }
        // BottomLeft
        if (tilePos.x == tilePosX - 1 && tilePos.y == tilePosY - 1)
        {
            Destroy(gameObject);
        }
        // BottomRight
        if (tilePos.x == tilePosX + 1 && tilePos.y == tilePosY - 1)
        {
            Destroy(gameObject);
        }
    }

    public void Randomize()
    {
        // Randomizing chosen condition to create opening for both X & Y values of attached tile
        tilePosX = Random.Range(-4, 4);
        tilePosY = Random.Range(-2, 2);
        randomized = true;
    }
}