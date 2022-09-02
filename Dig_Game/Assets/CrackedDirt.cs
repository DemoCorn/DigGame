using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedDirt : MonoBehaviour
{
    public float dirtHealth;
    
    public Sprite currentSprite;
    public Sprite[] crackedDirts;

    private SpriteRenderer rend;

    private void Start()
    {
        rend.color = new Color(0f, 0f, 0f);
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dirtHealth = GetComponentInParent<Player_Health>().GetHealth();

        if(dirtHealth <= 100 && dirtHealth >= 66)
        {
            rend.color = new Color(1.0f, 1.0f, 1.0f);
            currentSprite = crackedDirts[1];            
        }

        if (dirtHealth <= 66 && dirtHealth >= 33)
        {
            currentSprite = crackedDirts[2];
        }

        if (dirtHealth <= 33 && dirtHealth >= 0)
        {
            currentSprite = crackedDirts[3];
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = currentSprite;
    }
}
