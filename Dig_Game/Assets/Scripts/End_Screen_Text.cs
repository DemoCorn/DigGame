using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_Screen_Text : MonoBehaviour
{
    public Text gameText;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.IsWinning())
        {
            gameText.text = "You Win";
        }
        else
        {
            gameText.text = "You Lose";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
