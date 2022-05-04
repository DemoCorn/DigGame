using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    [SerializeField] Text Attack;
    [SerializeField] Text Dig;
    [SerializeField] Text Defence;
    [SerializeField] Text HP;

    // Update is called once per frame
    void Update()
    {
        Attack.text = "Attack: " + GameManager.Instance.GetPlayerAttack();
        Dig.text = "Dig: " + GameManager.Instance.GetPlayerDig();
        Defence.text = "Armor: " + GameManager.Instance.GetPlayerArmor();
        HP.text = "HP: " + GameManager.Instance.GetPlayerHealth().ToString() + "/" + GameManager.Instance.GetPlayerMaxHealth().ToString();
    }
}
