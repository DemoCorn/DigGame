using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{

    [Header("Reference to the boss minion prefabs:")]

    
    public GameObject bossMinion2;
    public GameObject bossMinion3;
    public GameObject bossMinion4;
    public GameObject bossMinion5;

    [Header("Reference to the boss prefabs: ")]

    public GameObject boss;
    public GameObject bossPhase2;
    public GameObject bossPhase3;

    [Header("Reference to animations for acid: ")]

    public GameObject acidGameObject;

    [SerializeField] private Animator acid;
    
    [Space(20)]

    //values for the phase 2 health bar
    private float maxBoss1Health;
    private float currentBoss1Health;

    private float maxBoss2Health;
    private float currentBoss2Health;

    private float maxBoss3Health;
    private float currentBoss3Health;

    private bool boss1Killed = false;
    private bool boss2Killed = false;
    private bool boss3Killed = false;

    [Header("Reference to UI elements:")]
    public BossHealthBar healthBar1;
    public BossHealthBar healthBar2;
    public BossHealthBar healthBar3;
    public GameObject healthBarUI;
    public GameObject skullIndicator;
    public Image healthColor1;
    public Image healthColor2;
    public Image healthColor3;
    [Space(20)]
    [Header("Reference to the Boss health:")]
    public Non_Player_Health boss1Health;
    public Non_Player_Health boss2Health;
    public Non_Player_Health boss3Health;
    [Space(20)]
    [Header("Reference to the reward room:")]
    public GameObject rewardRoom;
    public GameObject rewards;

   

    void Start()
    {

        rewardRoom = GameObject.Find("RewardRoom").gameObject;
        
        maxBoss1Health = boss1Health.GetHealth();
        maxBoss2Health = boss2Health.GetHealth();
        maxBoss3Health = boss3Health.GetHealth();

        currentBoss1Health = maxBoss1Health;
        healthBar1.setBossMaxHealth(maxBoss1Health);

        currentBoss2Health = maxBoss2Health;
        healthBar2.setBossMaxHealth(maxBoss2Health);

        currentBoss3Health = maxBoss3Health;
        healthBar3.setBossMaxHealth(maxBoss3Health);
    }


    public void Update()
    {
        //checking the players gear stats
        if (GameManager.Instance.GetPlayerAttack() <= 23 )
        {
            skullIndicator.SetActive(true);
           
            healthColor1.color = new Color32(103, 0, 191, 255);
            healthColor2.color = new Color32(103, 0, 191, 255);
            healthColor3.color = new Color32(103, 0, 191, 255);
        }
        else
        {
            skullIndicator.SetActive(false);
            
            healthColor1.color = new Color32(255, 40, 40, 255);
            healthColor2.color = new Color32(255, 40, 40, 255);
            healthColor3.color = new Color32(255, 40, 40, 255);
        }

        if (! boss3Killed)
        {
            if (bossPhase3 == null)
            {
                
                healthBarUI.SetActive(false);
                rewardRoom.SetActive(false);
                rewards.SetActive(true);
                acid.Play("AcidLowering");
                boss3Killed = true;
            }
        }
        
        if (! boss1Killed)
        {
            if (boss == null)
            {
                bossPhase2.SetActive(true);
                acid.Play("AcidRising_1");
                acidGameObject.SetActive(true);
                bossMinion2.SetActive(true);
                bossMinion3.SetActive(true);
                boss1Killed = true;
            }
        }

        if (! boss2Killed)
        {
            if (bossPhase2 == null)
            {
                bossPhase3.SetActive(true);
                acid.Play("AcidRising_2");
                bossMinion4.SetActive(true);
                bossMinion5.SetActive(true);
                boss2Killed = true;
            }
        }

        

        
        healthBar1.SetBossHealth(boss1Health.GetHealth());
        healthBar2.SetBossHealth(boss2Health.GetHealth());
        healthBar3.SetBossHealth(boss3Health.GetHealth());
    }

   

   

}
