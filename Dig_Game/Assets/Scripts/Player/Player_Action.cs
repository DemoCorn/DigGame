using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] Bomb_Projectile bomb;
    [SerializeField] GameObject digTool;

    void Start()
    {
        UpdateClass();
    }

    // Update is called once per frame
    void Update()
    {
       // Inputs inputs = GameManager.Instance.GetInputs();
        if (Input.GetKeyDown((KeyCode)KeyCode.Mouse3))
        {
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("bomb throw");
        Instantiate(bomb, transform.position, transform.rotation);
    }

    public void Dig()
    {

    }

    public void UpdateClass()
    {
        switch (GetComponent<Class_Manager>().currentClass)
        {
            case ClassType.Fighter:
                break;
            case ClassType.Gladiator:
                break;
            case ClassType.Engineer:
                break;
            default:
                break;
        }
    }
}
