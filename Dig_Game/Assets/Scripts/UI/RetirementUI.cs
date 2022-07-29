using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetirementUI : MonoBehaviour
{
    public void Retire()
    {
        GameManager.Instance.RetirePlayer();
    }

    public void CloseScreen()
    {
        GameManager.Instance.UIManager.SetRetireScreen();
    }
}
