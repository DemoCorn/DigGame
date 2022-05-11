using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WeaponStats : MonoBehaviour
{
    [SerializeField] float WeaponDamage;
    [SerializeField] float DigDamage;
    [SerializeField] Collider2D WeaponCollider;

    public void Equip(float attackChange, float digChange)
    {
        WeaponDamage += attackChange;
        DigDamage += digChange;
    }

    public float GetAttack()
    {
        return WeaponDamage;
    }

    public float GetDig()
    {
        return DigDamage;
    }

    public Collider2D GetWeaponHitbox()
    {
        return WeaponCollider;
    }
}
