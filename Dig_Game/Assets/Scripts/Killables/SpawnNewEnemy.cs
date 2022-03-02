using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewEnemy : MonoBehaviour
{

    [SerializeField] float spawnNewEnemyChance = 10.0f;
    [SerializeField] GameObject[] enemies;
    

    private void OnDestroy()
    {
        if (transform.tag == "BreakableBlock")
        {
            if (Random.Range(0.0f, 100.0f) <= spawnNewEnemyChance)
            {
                GameObject newEnemy = (GameObject)Instantiate(enemies[0], transform.position, Quaternion.identity);
                newEnemy.transform.localScale = new Vector2(0.5f, 0.5f);
            }
        }
    }
}
