using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Equipment_UI : MonoBehaviour
{
    [SerializeField] private GameObject[] equipmentObjects = new GameObject[Enum.GetNames(typeof(EquipmentType)).Length];
    private EquipmentSpace[] equipmentSlots = new EquipmentSpace[Enum.GetNames(typeof(EquipmentType)).Length];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(EquipmentType)).Length; i++)
        {
            equipmentSlots[i] = new EquipmentSpace(equipmentObjects[i].GetComponentInChildren<Image>(), i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Equipment[] equipment = GameManager.Instance.InventoryManager.GetEquipment();
        for (int i = 0; i < Enum.GetNames(typeof(EquipmentType)).Length; i++)
        {
            if (equipment[i] != null && equipmentSlots[i] != null)
            {
                equipmentSlots[i].slotImage.sprite = equipment[i].itemSprite;
            }
        }
    }

    public void Unequip(EquipmentType slotType)
    {
        GameManager.Instance.InventoryManager.Unequip(slotType);
    }

    public class EquipmentSpace
    {
        public EquipmentSpace()
        {
        }

        public EquipmentSpace(Image image, int index)
        {
            slotImage = image;
            buttonIndex = index;
        }

        public Image slotImage;
        public int buttonIndex;
    }
}
