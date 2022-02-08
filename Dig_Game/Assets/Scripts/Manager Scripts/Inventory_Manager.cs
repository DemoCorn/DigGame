using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System;

public class Inventory_Manager : MonoBehaviour
{
    [SerializeField] private PlayerClass playerClass = PlayerClass.None;
    [SerializeField] private List<ItemGroup> inventory = new List<ItemGroup>();

    [SerializeField] private Equipment[] noEquipment = new Equipment[Enum.GetNames(typeof(EquipmentType)).Length];
    [SerializeField] private Equipment[] equipment = new Equipment[Enum.GetNames(typeof(EquipmentType)).Length];

    [SerializeField] private Blueprint testBlueprint;

    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private List<InventorySpace> inventorySlots = new List<InventorySpace>();

    [SerializeField] private bool gridNeeded = true;
    [SerializeField] private GameObject grid;
    [SerializeField] private float gridOffset = 0.0f;

    private Inputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        // Equip the lack of armor and weapon, needed so that Equip works
        for (int i = 0; i < Enum.GetNames(typeof(EquipmentType)).Length; i++)
        {
            if (equipment[i] == null)
            {
                equipment[i] = noEquipment[i];
            }
        }
        inputs = GameManager.Instance.GetInputs();

        inventoryScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(inputs.inventoryOpen))
        {
            inventoryScreen.SetActive(!inventoryScreen.activeSelf);
        }

        
        if (inventoryScreen.activeSelf)
        {
            if (gridNeeded)
            {
                grid.transform.position = new Vector3(GameManager.Instance.GetCameraPosition().x + gridOffset, GameManager.Instance.GetCameraPosition().y, GameManager.Instance.GetCameraPosition().y);
            }
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventory.Count > i)
                {
                    inventorySlots[i].slotImage.sprite = inventory[i].item.itemSprite;
                    inventorySlots[i].slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    inventorySlots[i].amountText.text = inventory[i].amount.ToString();
                }
                else
                {
                    inventorySlots[i].slotImage.sprite = null;
                    inventorySlots[i].slotImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    inventorySlots[i].amountText.text = "";
                }
            }
        }
        */

        // Testing
        if(Input.GetKeyDown("r"))
        {
            Craft(testBlueprint);
        }
    }

    public void EditInventory(ItemGroup groupToAdd)
    {
        // Check whether an item needs to be added to the dictionary or the value associated with the key just needs to be changed
        int itemPlacement = InventoryHas(groupToAdd.item);

        if (itemPlacement != -1)
        {
            inventory[itemPlacement].amount += groupToAdd.amount;

            // Remove item if there is no more of them
            if (inventory[itemPlacement].amount < 1)
            {
                Assert.AreEqual(0, inventory[itemPlacement].amount, "Too many items were removed, inventory went into negative items");
                inventory.RemoveAt(itemPlacement);
            }
        }
        else
        {
            Assert.IsTrue(groupToAdd.amount > 0, "Tried to remove an item that does not exist in the inventory");
            inventory.Add(new ItemGroup(groupToAdd.item, groupToAdd.amount));
        }
    }

    public void Equip(Equipment newEquipment)
    {
        // Check to make sure the player class matches the equipment requirement
        if (playerClass == newEquipment.classRequirement || newEquipment.classRequirement == PlayerClass.None)
        {
            // Calculate the change in stats the change in equipment will have
            float fHealth = newEquipment.healthModifier - equipment[(int)newEquipment.equipmentType].healthModifier;
            float fArmor = newEquipment.armorModifier - equipment[(int)newEquipment.equipmentType].armorModifier;
            float fDamage = newEquipment.attackModifier - equipment[(int)newEquipment.equipmentType].attackModifier;
            float fDig = newEquipment.digModifier - equipment[(int)newEquipment.equipmentType].digModifier;

            // if the equipment being equiped is not the blank equipment used to make this function work, remove it from the inventory
            if (newEquipment != noEquipment[(int)newEquipment.equipmentType])
            {
                EditInventory(new ItemGroup(newEquipment, -1));
            }

            // if the equipment being unequiped is not the blank equipment used to make this function work, add it to the inventory
            if (equipment[(int)newEquipment.equipmentType] != noEquipment[(int)newEquipment.equipmentType])
            {
                EditInventory(new ItemGroup(equipment[(int)newEquipment.equipmentType], 1));
            }

            // Equip the new armor and change stats accordingly
            equipment[(int)newEquipment.equipmentType] = newEquipment;
            GameManager.Instance.EquipPlayer(fHealth, fArmor, fDamage, fDig);
        }
    }

    public bool Craft(Blueprint blueprint)
    {
        bool canCraft = true;

        // Iterate through all requirements of the blueprint checking if the craft is possible
        foreach (ItemGroup requirements in blueprint.recipe)
        {
            int itemPlacement = InventoryHas(requirements.item);
            if (itemPlacement == -1)
            {
                canCraft = false;
                break;
            }
            else if (inventory[itemPlacement].amount < requirements.amount)
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
                requirement.amount *= -1; // swap back to maintain the blueprint
            }
            EditInventory(blueprint.result);
            return true;
        }
        return false;
    }

    // Returns where in the list an item is, if the item does not exist returns -1
    private int InventoryHas(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == item)
            {
                return i;
            }
        }
        return -1;
    }

    public void ClickEquip(int slotNum)
    {
        if (inventory[slotNum].item is Equipment)
        {
            Equip((Equipment)inventory[slotNum].item);
        }
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

[System.Serializable]
public class InventorySpace
{
    public InventorySpace()
    {
    }

    public InventorySpace(Image key, Text value)
    {
        slotImage = key;
        amountText = value;
    }

    public Image slotImage;
    public Text amountText;
}

public enum ItemType
{
    item = 0,
    equipment = 1
}

public enum EquipmentType
{
    weapon = 0,
    head = 1,
    chest = 2,
    legs = 3
}

public enum PlayerClass
{
    None = 0,
    Warrior = 1,
    Rogue = 2,
    Wizard = 3
}