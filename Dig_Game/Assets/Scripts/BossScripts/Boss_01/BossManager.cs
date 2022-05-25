using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject boss1;
    public GameObject boss2;

    //values for the phase 1 health bar
    public float maxBoss1Health;
    public float currentBoss1Health;
    //values for the phase 2 health bar
    public float maxBoss2Health;
    public float currentBoss2Health;

    public BossHealthBar healthBar;
    public BossHealthBar healthBar2;

    public GameObject healthBarUI;

    public Non_Player_Health boss1Health;
    public Non_Player_Health boss2Health;

    public GameObject rewardRoom;
    public GameObject rewards;

    void Start()
    {

        rewardRoom = GameObject.Find("RewardRoom").gameObject;
        

        

        maxBoss1Health = 150;
        maxBoss2Health = 150;

        currentBoss1Health = maxBoss1Health;
        healthBar.setBossMaxHealth(maxBoss1Health);

        currentBoss2Health = maxBoss2Health;
        healthBar2.setBossMaxHealth(maxBoss2Health);
    }


    public void Update()
    {
        if (boss1 == null)
        {
            boss2.SetActive(true);
            
        }
        if (boss2 == null)
        {
            //Debug.Log("You Win");
            //healthBarUI.SetActive(false);
            //rewardRoom.SetActive(false);
        }
        if (boss2Health.GetHealth() <= 0)
        {
            Debug.Log("You Win");
            healthBarUI.SetActive(false);
            rewardRoom.SetActive(false);
            rewards.SetActive(true);
        }

        healthBar.SetBossHealth(boss1Health.GetHealth());
        healthBar2.SetBossHealth(boss2Health.GetHealth());
    }

}
