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
        // Equip the lack of armor and weapon, needed so that Equip works
        equipment[0] = noWeapon;
        equipment[1] = noHelmet;
        equipment[2] = noChest;
        equipment[3] = noLeggings;
    }

    // Update is called once per frame
    void Update()
    {
        // Testing
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
        // Check whether an item needs to be added to the dictionary or the value associated with the key just needs to be changed
        if(inventory.ContainsKey(groupToAdd.item))
        {
            inventory[groupToAdd.item] += groupToAdd.amount;

            // Remove item if there is no more of them
            if (inventory[groupToAdd.item] < 1)
            {
                if (inventory[groupToAdd.item] != 0)
                {
                    Debug.LogWarning("Too many items were removed, inventory went into negative items");
                }
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
        // Calculate the change in stats the change in equipment will have
        float fHealth = newEquipment.healthModifier - equipment[(int)newEquipment.equipmentType].healthModifier;
        float fArmor = newEquipment.armorModifier - equipment[(int)newEquipment.equipmentType].armorModifier;
        float fDamage = newEquipment.attackModifier - equipment[(int)newEquipment.equipmentType].attackModifier;

        // if the equipment equiped is not the blank equipment used to make this function work, add it to the inventory
        if (equipment[(int)newEquipment.equipmentType] != noWeapon && equipment[(int)newEquipment.equipmentType] != noHelmet && equipment[(int)newEquipment.equipmentType] != noChest && equipment[(int)newEquipment.equipmentType] != noLeggings)
        {
            EditInventory(new ItemGroup(equipment[(int)newEquipment.equipmentType], 1));
        }

        // Equip the new armor and change stats accordingly
        equipment[(int)newEquipment.equipmentType] = newEquipment;
        GameManager.Instance.EquipPlayer(fHealth, fArmor, fDamage);
    }

    public bool Craft(Blueprint blueprint)
    {
        bool canCraft = true;

        // Iterate through all requirements of the blueprint checking if the craft is possible
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

        // Craft the item
        if (canCraft)
        {
            foreach (ItemGroup requirement in blueprint.recipe)
            {
                requirement.amount *= -1; // need to swap the amount to negative so that items are removed from the inventory
                EditInventory(requirement);
            }
            EditInventory(blueprint.result);
            return true;
        }
        return false;
    }
}

// This is basically just an Item int pair however those don't show up in the unity editor, this will
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
