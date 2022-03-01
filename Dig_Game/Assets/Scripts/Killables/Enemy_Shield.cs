using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shield : MonoBehaviour
{
    [SerializeField] float shieldTimeMin = 2.0f;
    [SerializeField] float shieldTimeMax = 4.0f;
    [SerializeField] float shieldDelayMin = 2.0f;
    [SerializeField] float shieldDelayMax = 4.0f;
    float shieldTimer = 0;
    float shieldDelayTimer = 0;
    bool isShielded = false;

    GameObject shield;
    SpriteRenderer shieldSprite;

   
    private void Awake()
    {
        shield = transform.Find("Shield").gameObject;
        shieldSprite = shield.GetComponent<SpriteRenderer>();
        shieldTimer = Random.Range(shieldTimeMin, shieldTimeMax);
        shieldDelayTimer = Random.Range(shieldDelayMin, shieldDelayMax);
    }

    private void Update()
    {
        shieldTimer -= Time.deltaTime;
        shieldDelayTimer -= Time.deltaTime;

        if (!isShielded && shieldDelayTimer <= 0)
        {
            ShieldActivate();            
        }
        if (isShielded && shieldTimer <= 0)
        {
            ShieldDeactivate();            
        }

    }

    private void ShieldActivate()
    {
        shieldSprite.enabled = true;
        GetComponent<Non_Player_Health>().mImmune = true;
        shieldTimer = Random.Range(shieldTimeMin, shieldTimeMax);
        isShielded = true;      
    }
    public void ShieldDeactivate()
    {
        shieldSprite.enabled = false;
        GetComponent<Non_Player_Health>().mImmune = false;
        shieldDelayTimer = Random.Range(shieldDelayMin, shieldDelayMax);
        isShielded = false;
    }

}
