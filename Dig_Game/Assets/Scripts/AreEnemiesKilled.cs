using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreEnemiesKilled : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject Blockade;
    public int EnemiesAlive;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

        
        foreach (GameObject enemies in Enemies)
        {
            if (enemies.activeSelf == false)
            {
                EnemiesAlive --;
            }

        }
        if (EnemiesAlive == 0)
        {
            Blockade.SetActive(false);
        }
        Debug.Log(EnemiesAlive);

    }
}
