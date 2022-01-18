using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Items/Equipment", order = 2)]
public class Equipment : Item
{
    public EquipmentType equipmentType;
    public PlayerClass classRequirement;
    public float healthModifier;
    public float armorModifier;
    public float attackModifier;
    public float digModifier;

    new protected const ItemType itemType = ItemType.equipment;
}
