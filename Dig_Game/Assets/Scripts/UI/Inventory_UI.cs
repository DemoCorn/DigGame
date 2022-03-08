using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory_UI : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotObjects = new List<GameObject>();
    private List<InventorySpace> inventoryButtons = new List<InventorySpace>();

    [SerializeField] private GameObject[] equipmentObjects = new GameObject[Enum.GetNames(typeof(EquipmentType)).Length];
    private InventorySpace[] equipmentSlots = new InventorySpace[Enum.GetNames(typeof(EquipmentType)).Length];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotObjects.Count; i++)
        {
            inventoryButtons.Add(new InventorySpace(slotObjects[i].GetComponentInChildren<Image>(), slotObjects[i].GetComponentInChildren<Text>(), i));
        }
        /*
        for (int i = 0; i < Enum.GetNames(typeof(EquipmentType)).Length; i++)
        {
            equipmentSlots[i] = new InventorySpace(slotObjects[i].GetComponentInChildren<Image>(), slotObjects[i].GetComponentInChildren<Text>(), i);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        Equipment[] equipment = GameManager.Instance.InventoryManager.GetEquipment();
        List<ItemGroup> inventory = GameManager.Instance.InventoryManager.GetInventory();

        /*
        for (int i = 0; i < Enum.GetNames(typeof(EquipmentType)).Length; i++)
        {
            if (equipment[i] != null && equipmentSlots[i] != null)
            {
                equipmentSlots[i].slotImage.sprite = equipment[i].itemSprite;
            }
        }
        */
        for (int i = 0; i < inventoryButtons.Count; i++)
        {
            if (inventory.Count > i)
            {
                inventoryButtons[i].slotImage.sprite = inventory[i].item.itemSprite;
                inventoryButtons[i].slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                inventoryButtons[i].amountText.text = inventory[i].amount.ToString();
            }
            else
            {
                inventoryButtons[i].slotImage.sprite = null;
                inventoryButtons[i].slotImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                inventoryButtons[i].amountText.text = "";
            }
        }
    }

    public void ClickEquip(int slotNum)
    {
        List<ItemGroup> inventory = GameManager.Instance.InventoryManager.GetInventory();
        if (inventory[slotNum].item is Equipment)
        {
            GameManager.Instance.InventoryManager.Equip((Equipment)inventory[slotNum].item);
        }
    }

    public class InventorySpace
    {
        public InventorySpace()
        {
        }

        public InventorySpace(Image image, Text text, int index)
        {
            slotImage = image;
            amountText = text;
            buttonIndex = index;
        }

        public Image slotImage;
        public Text amountText;
        public int buttonIndex;
    }
}