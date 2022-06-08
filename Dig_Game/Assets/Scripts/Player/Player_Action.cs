using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action : UsableEffect
{
    [SerializeField] GameObject weapon;
    [SerializeField] Bomb_Projectile bomb;
    [SerializeField] GameObject digTool;
    float cooldownTime = 60.0f;

    private int bombsAmmo = 0;

    void Start()
    {
        UpdateClass();
    }

    // Update is called once per frame
    void Update()
    {
    }

    override public float Activate()
    {
        Attack();
        return cooldownTime;
    }

    public void Attack()
    {
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

    public void AddBomb(int bomb)
    {
        bombsAmmo += bomb;
    }
}
