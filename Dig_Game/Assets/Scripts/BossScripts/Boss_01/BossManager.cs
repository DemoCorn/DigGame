using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public GameObject boss1;
    public GameObject boss2;

    //values for the phase 1 health bar
    private float maxBoss1Health;
    private float currentBoss1Health;
    //values for the phase 2 health bar
    private float maxBoss2Health;
    private float currentBoss2Health;

    public BossHealthBar healthBar;
    public BossHealthBar healthBar2;

    public GameObject healthBarUI;

    public Non_Player_Health boss1Health;
    public Non_Player_Health boss2Health;

    public Player_WeaponStats attackStat;

    public GameObject skullIndicator;

    public Image healthColor1;
    public Image healthColor2;

    public GameObject rewardRoom;
    public GameObject rewards;

    void Start()
    {

        rewardRoom = GameObject.Find("RewardRoom").gameObject;
        
        
        maxBoss1Health = 250;
        maxBoss2Health = 250;

        currentBoss1Health = maxBoss1Health;
        healthBar.setBossMaxHealth(maxBoss1Health);

        currentBoss2Health = maxBoss2Health;
        healthBar2.setBossMaxHealth(maxBoss2Health);
    }


    public void Update()
    {
        //checking the players gear stats
        if ( attackStat.GetAttack() <= 23 )
        {
            skullIndicator.SetActive(true);
            healthColor1.color = new Color32(103, 0, 191, 255);
            healthColor2.color = new Color32(103, 0, 191, 255);
        }
        else
        {
            skullIndicator.SetActive(false);
            healthColor1.color = new Color32(255, 40, 40, 255);
            healthColor2.color = new Color32(255, 40, 40, 255);
        }


        //checking if boss 1 is dead and starting second phase
        if (boss1 == null)
        {
            boss2.SetActive(true);
            
        }

        //finishing the boss battle
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
