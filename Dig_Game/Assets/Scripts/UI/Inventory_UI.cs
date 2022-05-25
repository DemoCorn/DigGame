using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory_UI : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotObjects = new List<GameObject>();
    private List<InventorySpace> inventoryButtons = new List<InventorySpace>();

    [SerializeField] private Text descriptionBox;
    [SerializeField] private Vector3 descriptionBoxOffset;
    int currentHoverIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotObjects.Count; i++)
        {
            inventoryButtons.Add(new InventorySpace(slotObjects[i].GetComponentInChildren<Image>(), slotObjects[i].GetComponentInChildren<Text>(), i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<ItemGroup> inventory = GameManager.Instance.InventoryManager.GetInventory();

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

        if (currentHoverIndex > -1)
        {
            descriptionBox.transform.position = Input.mousePosition + descriptionBoxOffset;
        }
    }

    public void ClickEquip(int slotNum)
    {
        List<ItemGroup> inventory = GameManager.Instance.InventoryManager.GetInventory();
        if (inventory.Count > slotNum)
        {
            if (inventory[slotNum].item is Equipment)
            {
                GameManager.Instance.InventoryManager.Equip((Equipment)inventory[slotNum].item);
            }
            if (inventory[slotNum].item is Usable)
            {
                GameManager.Instance.InventoryManager.EquipUsable((Usable)inventory[slotNum].item);
            }
        }
    }

    public void PointerEnter(int slotNum)
    {
        List<ItemGroup> inventory = GameManager.Instance.InventoryManager.GetInventory();
        if (inventory.Count > slotNum)
        {
            currentHoverIndex = slotNum;
            descriptionBox.text = inventory[slotNum].item.itemDescription;
        }
    }

    public void PointerExit(int slotNum)
    {
        if (currentHoverIndex == slotNum)
        {
            currentHoverIndex = -1;
            descriptionBox.text = "";
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
