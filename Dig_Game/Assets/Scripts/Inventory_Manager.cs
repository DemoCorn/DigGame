using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class Inventory_Manager : MonoBehaviour
{
    [SerializeField] private Equipment noWeapon;
    [SerializeField] private Equipment noHelmet;
    [SerializeField] private Equipment noChest;
    [SerializeField] private Equipment noLeggings;

    [SerializeField] private Blueprint test;
    [SerializeField] private Equipment test2;

    private Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    private Equipment[] equipment = new Equipment[Enum.GetNames(typeof(EquipmentType)).Length];

    // Start is called before the first frame update
    void Start()
    {
        equipment[0] = noWeapon;
        equipment[1] = noHelmet;
        equipment[2] = noChest;
        equipment[3] = noLeggings;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            if(Craft(test))
            {
                Equip(test2);
            }
        }
    }

    public void EditInventory(ItemGroup groupToAdd)
    {
        if(inventory.ContainsKey(groupToAdd.item))
        {
            inventory[groupToAdd.item] += groupToAdd.amount;
            if (inventory[groupToAdd.item] < 1)
            {
                inventory.Remove(groupToAdd.item);
            }
        }
        else if (groupToAdd.amount > 0)
        {
            inventory.Add(groupToAdd.item, groupToAdd.amount);
        }
        else
        {
            Debug.LogWarning("Tried to remove an item that does not exist in the inventory");
        }
    }

    public void Equip(Equipment newEquipment)
    {
        float health = newEquipment.healthModifier - equipment[(int)newEquipment.equipmentType].healthModifier;
        float armor = newEquipment.armorModifier - equipment[(int)newEquipment.equipmentType].armorModifier;
        float damage = newEquipment.attackModifier - equipment[(int)newEquipment.equipmentType].attackModifier;

        if (equipment[(int)newEquipment.equipmentType] != noWeapon && equipment[(int)newEquipment.equipmentType] != noHelmet && equipment[(int)newEquipment.equipmentType] != noChest && equipment[(int)newEquipment.equipmentType] != noLeggings)
        {
            EditInventory(new ItemGroup(equipment[(int)newEquipment.equipmentType], 1));
        }

        equipment[(int)newEquipment.equipmentType] = newEquipment;


        GameManager.Instance.EquipPlayer(health, armor, damage);
    }

    public bool Craft(Blueprint blueprint)
    {
        bool canCraft = true;
        foreach (ItemGroup requirements in blueprint.recipe)
        {
            if (!inventory.ContainsKey(requirements.item))
            {
                canCraft = false;
                break;
            }
            else if (inventory[requirements.item] < requirements.amount)
            {
                canCraft = false;
                break;
            }
        }

        if (canCraft)
        {
            foreach (ItemGroup requirement in blueprint.recipe)
            {
                EditInventory(requirement);
            }
            EditInventory(blueprint.result);
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class ItemGroup
{
    public ItemGroup()
    {
    }

    public ItemGroup(Item key, int value)
    {
        item = key;
        amount = value;
    }

    public Item item;
    public int amount;
}
