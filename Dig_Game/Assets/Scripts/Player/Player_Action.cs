using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action : MonoBehaviour
{
    GameObject weapon;
    GameObject digTool;
    void Start()
    {
        UpdateClass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {

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
