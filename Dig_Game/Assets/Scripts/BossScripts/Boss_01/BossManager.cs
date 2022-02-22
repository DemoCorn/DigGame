using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject boss1;
    public GameObject boss1Hitbox;

    public GameObject boss2;


    public void Start()
    {
        
    }

    public void Update()
    {
        if (boss1.activeInHierarchy == false)
        {
            boss2.SetActive(true);
            
        }
    }

}
