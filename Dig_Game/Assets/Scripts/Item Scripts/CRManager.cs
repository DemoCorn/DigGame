using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CRManager : MonoBehaviour
{
    [Header("Gate")]
    public GameObject[] tiles;
    private bool hasBeenActivated = false;

    [Header("Spawner")]
    public List<GameObject> enemySpawnLocations;

    [Header("Enemy Count")]
    public int enemyCount;

    [Header("Treasure")]
    public RewardBoxes[] rewardBoxes;

    //etai
    public AudioSource casualMusic;
    public AudioSource combatMusic;


    void Start()
    {
        foreach (GameObject spawnlocation in enemySpawnLocations)
        {
            spawnlocation.SetActive(false);
        }

        casualMusic = GameObject.Find("Music Player").GetComponent<AudioSource>();
        combatMusic = GameObject.Find("Combat Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        

        //Open Gate
        if(hasBeenActivated == true)
        {
            for (int i = 0; i < enemySpawnLocations.Count; i++)
            {
                if (!enemySpawnLocations[i])
                {
                    enemySpawnLocations.RemoveAt(i);
                    enemyCount--;
                    i--;
                }
            }

            if (enemyCount <= 0)
            {
                StartCoroutine(FadeIn(casualMusic, 1.0f));
                StartCoroutine(FadeOut(combatMusic, 1.0f));

                gameObject.SetActive(false);
            }
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

        //begin combat music
        StartCoroutine(FadeOut(casualMusic, 1.0f));
        StartCoroutine(FadeIn(combatMusic, 1.0f));
    }
    public void SpawnEnemies()
    {
        foreach (GameObject spawnlocation in enemySpawnLocations)
        {
            spawnlocation.SetActive(true);
        }

        //Update EnemyCount
        enemyCount = enemySpawnLocations.Count;
    }

    //Music fade in and out
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 1.0f;

        audioSource.Play();

        while (audioSource.volume < 0.4f);
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        //audioSource.Stop();
        audioSource.volume = startVolume;
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