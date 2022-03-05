using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStunScript : MonoBehaviour
{
    public GameObject boss;

    public Transform player;
    public bool isStunned;

    public float stunTimer;
    public float stunRequirement;

    // Start is called before the first frame update
    void Start()
    {
        stunTimer = 0;
        boss.GetComponent<Non_Player_Health>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned == true)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer <= stunRequirement)
            {
                
                //player can now damage
                boss.GetComponent<Non_Player_Health>().enabled = true;

            }
            if (stunTimer >= stunRequirement)
            {
                isStunned = false;
                boss.GetComponent<Non_Player_Health>().enabled = false;
                stunTimer = 0;

            }
        }

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Debris"))
        {
            isStunned = true;
        }
    }
}
