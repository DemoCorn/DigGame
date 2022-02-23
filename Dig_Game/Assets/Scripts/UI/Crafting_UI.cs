using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting_UI : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotObjects = new List<GameObject>();
    private List<CraftingSlot> slotSpaces = new List<CraftingSlot>();
    [SerializeField] private GameObject resultObject;
    private CraftingSlot resultSpace;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotObjects.Count; i++)
        {
            slotSpaces.Add(new CraftingSlot(slotObjects[i].GetComponentInChildren<Image>(), slotObjects[i].GetComponentInChildren<Text>(), i));
        }
        resultSpace = new CraftingSlot(resultObject.GetComponentInChildren<Image>(), resultObject.GetComponentInChildren<Text>(), 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            ChooseBlueprint(0);
        }
    }

    public void ChooseBlueprint(int blueprintIndex)
    {
        UnlockableBlueprint blueprint = GameManager.Instance.InventoryManager.GetBlueprint(blueprintIndex);

        resultSpace.slotImage.sprite = blueprint.blueprint.result.item.itemSprite;
        resultSpace.slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        resultSpace.amountText.text = blueprint.blueprint.result.item.itemName;

        foreach (CraftingSlot slot in slotSpaces)
        {
            if (blueprint.blueprint.recipe.Count > slot.slotIndex)
            {
                slot.slotImage.sprite = blueprint.blueprint.recipe[slot.slotIndex].item.itemSprite;
                slot.slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                slot.amountText.text = blueprint.blueprint.recipe[slot.slotIndex].item.itemName + ": " + blueprint.blueprint.recipe[slot.slotIndex].amount.ToString();
            }
            else
            {
                slot.slotImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                slot.amountText.text = "";
            }
        }
    }

    public class CraftingSlot
    {
        public CraftingSlot()
        {
        }

        public CraftingSlot(Image image, Text text, int index)
        {
            slotImage = image;
            amountText = text;
            slotIndex = index;
        }

        public Image slotImage;
        public Text amountText;
        public int slotIndex;
    }
}
