using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRManager : MonoBehaviour
{
    [Header("Gate")]
    public GameObject[] tiles;
    private bool hasBeenActivated = false;

    [Header("Spawner")]
    public List<GameObject> enemySpawns;

    [Header("Enemy Count")]
    public int enemyCount;

    [Header("Treasure")]
    public RewardBoxes[] rewardBoxes;

    void Start()
    {
        foreach (GameObject spawnlocation in enemySpawns)
        {
            spawnlocation.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBeenActivated == true && enemyCount <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("player has exited");
        if (hasBeenActivated == false && other.gameObject.CompareTag("Player"))
        {
            Invoke("SpawnEnemies", 0.3f);
            Invoke("CloseGate", 0.3f);
        }
    }

    public void CloseGate()
    {
            //Set layer to platform layer
            gameObject.layer = 3;

            //Set each tile in tile array to active
            foreach (GameObject tile in tiles)
            {
                tile.SetActive(true);
            }
            //Set hasBeenActivated to true, so the function does not call again;spawning more enemies in the process. 
            hasBeenActivated = true;
        
    }
    public void SpawnEnemies()
    {
        foreach (GameObject spawnlocation in enemySpawns)
        {
            spawnlocation.SetActive(true);
        }

        //Update EnemyCount
        enemyCount = enemySpawns.Count;
    }
}


[System.Serializable]
public class EnemySpawnLocations
{
    [Tooltip("The location you want your enemy to spawn.(Use an empty game object for this.)")]
    public GameObject enemySpawnLocation;
    [Tooltip("The enemy you want to spawn from the Enemies array)(0 = 1, 1 = 2, 2 = 3, etc, As arrays always start from 0)")]
    public int enemyToSpawn;
}

[System.Serializable]
public class RewardBoxes
{
    [Tooltip("The reward box you're spawning)")]
    public GameObject rewardBox;
    [Tooltip("The of the reward box.(Same as the enemies use an empty gameobject for this.)")]
    public GameObject rewardBoxLocation;
}