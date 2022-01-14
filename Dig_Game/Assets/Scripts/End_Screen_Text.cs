using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_Screen_Text : MonoBehaviour
{
    public Text messageText;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.IsWinning())
        {
            messageText.text = "You Win";
        }
        else
        {
            messageText.text = "You Lose";
        }
    }
}
