using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{

    [Header("Reference to the boss and minion prefab:")]
    public GameObject boss1;
    public GameObject boss2;
    [Space(20)]

    public Transform phase2Location;
    
    private bool phase1Complete = false;
    private bool phase2Complete = false;

    //values for the phase 2 health bar
    private float maxBoss2Health;
    private float currentBoss2Health;

    [Header("Reference to UI elements:")]
    public BossHealthBar healthBar2;
    public GameObject healthBarUI;
    public GameObject skullIndicator;
    public Image healthColor2;
    [Space(20)]
    [Header("Reference to the Boss health:")]
    public Non_Player_Health boss2Health;
    [Space(20)]
    [Header("Reference to the reward room:")]
    public GameObject rewardRoom;
    public GameObject rewards;

   

    void Start()
    {

        rewardRoom = GameObject.Find("RewardRoom").gameObject;
        
        maxBoss2Health = 500;

        currentBoss2Health = maxBoss2Health;
        healthBar2.setBossMaxHealth(maxBoss2Health);
    }


    public void Update()
    {
        //checking the players gear stats
        if (GameManager.Instance.GetPlayerAttack() <= 23 )
        {
            skullIndicator.SetActive(true);
           
            healthColor2.color = new Color32(103, 0, 191, 255);
        }
        else
        {
            skullIndicator.SetActive(false);
            
            healthColor2.color = new Color32(255, 40, 40, 255);
        }

        
        if (boss2Health.GetHealth() <= 0)
        {
            Debug.Log("You Win");
            healthBarUI.SetActive(false);
            rewardRoom.SetActive(false);
            rewards.SetActive(true);
        }

        if (! phase1Complete)
        {
            if (boss2Health.GetHealth() <= 350)
            {
                boss1.SetActive(true);
                boss2.transform.position = phase2Location.transform.position;
                phase1Complete = true;
            }
        }
        

        
        healthBar2.SetBossHealth(boss2Health.GetHealth());
    }

   

   

}
