using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Items/Equipment", order = 2)]
public class Equipment : Item
{
    //new protected const ItemType itemType = ItemType.equipment;
    public EquipmentType equipmentType;
    public float healthModifier;
    public float armorModifier;
    public float attackModifier;
}

public enum EquipmentType
{
    weapon = 0,
    head = 1,
    chest = 2,
    legs = 3
}
