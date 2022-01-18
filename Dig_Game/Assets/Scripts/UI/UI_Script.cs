using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public Text pointsText;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "Score: " + GameManager.Instance.GetScore().ToString();
        healthText.text = "HP: " + GameManager.Instance.GetPlayerHealth().ToString();
    }
}
