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
    private PlayerClass lastClass;

    [SerializeField] private List<ItemGroup> inventory = new List<ItemGroup>();

    [Header("Equipment")]
    [SerializeField] private Equipment[] noEquipment = new Equipment[Enum.GetNames(typeof(EquipmentType)).Length];
    [SerializeField] private Equipment[] equipment = new Equipment[Enum.GetNames(typeof(EquipmentType)).Length];

    [Header("Usables")]
    public UsableGroup[] EquipedUsables = new UsableGroup[3] {new UsableGroup(null, 0), new UsableGroup(null, 0), new UsableGroup(null, 0) };

    [SerializeField] private List<UnlockableBlueprint> blueprints = new List<UnlockableBlueprint>();

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

        // Randomize class
        playerClass = (PlayerClass)(int)UnityEngine.Random.Range(1, System.Enum.GetValues(typeof(PlayerClass)).Length);
        // Remember last class so it won't repeat upon death
        lastClass = playerClass;
    }

    public void BootUp()
    {
        ReEquip();
    }

    private void Update()
    {
        if (GameManager.Instance.isMainLevel)
        {
            Inputs inputs = GameManager.Instance.InputManager.GetInputs();
            for (int i = 0; i < EquipedUsables.Length; i++)
            {
                if (EquipedUsables[i].usable != null)
                {
                    if (Input.GetKeyDown(inputs.useUsables[i]) && !EquipedUsables[i].cooldown)
                    {
                        if (EquipedUsables[i].amount > 0)
                        {
                            float cooldownTime = EquipedUsables[i].usable.effect.GetComponent<UsableEffect>().Activate();
                            EquipedUsables[i].amount--;
                            EquipedUsables[i].cooldown = true;
                            StartCoroutine(CooldownStop(i, cooldownTime));
                        }
                    }
                }
            }
        }
    }

    public void EditInventory(ItemGroup groupToAdd)
    {
        // Check whether an item is in one of the equiped usable slots and if so add it there instead
        if (groupToAdd.item is Usable && groupToAdd.amount > 0)
        {
            foreach(UsableGroup usable in EquipedUsables)
            {
                if (usable.usable == (Usable)groupToAdd.item)
                {
                    usable.amount += groupToAdd.amount;
                    return;
                }
            }
        }
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

    public void Unequip(EquipmentType equipment)
    {
        Equip(noEquipment[(int)equipment]);
    }

    public void ReEquip()
    {
        foreach (Equipment eq in equipment)
        {
            if (eq != null)
            {
                float fHealth = eq.healthModifier;
                float fArmor = eq.armorModifier;
                float fDamage = eq.attackModifier;
                float fDig = eq.digModifier;

                GameManager.Instance.EquipPlayer(fHealth, fArmor, fDamage, fDig);
            }
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

    public PlayerClass getPlayerClass()
    {
        return playerClass;
    }

    public void AddBlueprint(Blueprint blueprint)
    {
        UnlockableBlueprint addblueprint = blueprints.Find(x => x.blueprint == blueprint);

        if (addblueprint != null)
        {
            addblueprint.isUnlocked = true;
            GameManager.Instance.UIManager.DisplayBlueprint(blueprint);
            Debug.Log(addblueprint.blueprint.result.item.name);
            return;
        }
        Debug.LogWarning("Blueprint dropped not contained in Inventory_Manager");
    }

    public bool BlueprintUnlocked(Blueprint blueprint)
    {
        UnlockableBlueprint returnvalue = blueprints.Find(x => x.blueprint == blueprint);

        if (returnvalue != null)
        {
            return returnvalue.isUnlocked;
        }
        Debug.LogWarning("Blueprint in drop script not contained in Inventory_Manager");
        return false;
    }

    public List<ItemGroup> GetInventory()
    {
        return inventory;
    }

    public Equipment[] GetEquipment()
    {
        return equipment;
    }

    public UsableGroup[] GetUsable()
    {
        return EquipedUsables;
    }    

    public ref List<UnlockableBlueprint> GetBlueprints()
    {
        return ref blueprints;
    }

    public void DieReset()
    {
        for (int i = 0; i < Enum.GetNames(typeof(EquipmentType)).Length; i++)
        {
            Unequip(equipment[i].equipmentType);
        }

        for (int i = 0; i < 3; ++i)
        {
            EquipedUsables[i] = new UsableGroup(null, 0);
        }

        inputs = GameManager.Instance.GetInputs();

        inventory.Clear();
    }

    public void RandomizeClass()
    {
        // Make sure last class is not repeated
        while (lastClass == playerClass)
        {
            playerClass = (PlayerClass)(int)UnityEngine.Random.Range(1, System.Enum.GetValues(typeof(PlayerClass)).Length);
        }
        // Save last class
        lastClass = playerClass;
    }

    IEnumerator CooldownStop(int index, float time)
    {
        yield return new WaitForSeconds(time);
        EquipedUsables[index].cooldown = false;
    }
}

// This is basically just an Item int pair however those don't show up in the unity editor, this will
[System.Serializable]
public class ItemGroup
{
    public ItemGroup()
    {
    }

    public ItemGroup(UsableGroup usables)
    {
        item = usables.usable;
        amount = usables.amount;
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
public class UsableGroup
{
    public UsableGroup()
    {
    }

    public UsableGroup(ItemGroup items)
    {
        usable = (Usable)items.item;
        amount = items.amount;
    }

    public UsableGroup(Usable key, int value)
    {
        usable = key;
        amount = value;
    }
    public Usable usable;
    public int amount;
    public bool cooldown = false;
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

[System.Serializable]
public class UnlockableBlueprint
{
    public UnlockableBlueprint()
    {
    }

    public UnlockableBlueprint(Blueprint key, bool value)
    {
        blueprint = key;
        isUnlocked = value;
    }

    public Blueprint blueprint;
    public bool isUnlocked;
}

public enum ItemType
{
    item = 0,
    equipment = 1
}

public enum EquipmentType
{
    pickaxe = 0,
    head = 1,
    chest = 2,
    legs = 3,
    gauntlets = 4,
    weapon = 5
}

public enum PlayerClass
{
    None = 0,
    Warrior = 1,
    Rogue = 2,
    Wizard = 3
}