using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Equipment_Choice : MonoBehaviour
{
    [SerializeField] GameObject[] equipment = new GameObject[2];

    Weapon_Attack weapon;
    Player_Attack dig;

    // Start is called before the first frame update
    void Start()
    {
        equipment[0].SetActive(true);
        equipment[1].SetActive(false);

        dig = equipment[0].GetComponent<Player_Attack>();
        weapon = equipment[1].GetComponent<Weapon_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs inputs = GameManager.Instance.GetInputs();

        if (Input.GetKeyDown((KeyCode)inputs.dig))
        {
            equipment[0].SetActive(true);
            equipment[1].SetActive(false);
            dig.Attack();
        }
        if (Input.GetKeyDown((KeyCode)inputs.attack))
        {
            equipment[1].SetActive(true);
            equipment[0].SetActive(false);
            weapon.Attack();
        }
    }
}
