using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting_UI : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotObjects = new List<GameObject>();
    private List<CraftingSlot> slotSpaces = new List<CraftingSlot>();
    private List<CraftingSlot> defaultSlotSpaces = new List<CraftingSlot>();

    [SerializeField] private GameObject resultObject;
    private ResultSlot resultSpace;
    private ResultSlot defaultResultSpace;

    private Blueprint currentBlueprint;

    public GameObject blueprintButton;
    public GameObject blueprintList;

    void Start()
    {
        for (int i = 0; i < slotObjects.Count; i++)
        {
            slotSpaces.Add(new CraftingSlot(slotObjects[i].GetComponentInChildren<Image>(), slotObjects[i].GetComponentInChildren<Text>(), i));
        }
        ResultSlotText resultText = resultObject.GetComponentInChildren<ResultSlotText>();
        resultSpace = new ResultSlot(resultObject.GetComponentInChildren<Image>(), resultText.nameText, resultText.hpText, resultText.defenceText, resultText.digText, resultText.attackText);

        GameObject newButton;
        List<UnlockableBlueprint> blueprints = GameManager.Instance.InventoryManager.GetBlueprints();
        Blueprint_UI bpUI;

        newButton = blueprintButton;
        bpUI = newButton.GetComponent<Blueprint_UI>();
        bpUI.blueprint = blueprints[0];
        bpUI.index = 0;
        newButton.GetComponentInChildren<Text>().text = bpUI.blueprint.blueprint.result.item.itemName;

        for (int i = 1; i < blueprints.Count; i++)
        {
            newButton = Instantiate(blueprintButton, blueprintList.transform);
            bpUI = newButton.GetComponent<Blueprint_UI>();
            bpUI.blueprint = blueprints[i];
            bpUI.index = i;
            newButton.GetComponentInChildren<Text>().text = bpUI.blueprint.blueprint.result.item.itemName;
            newButton.GetComponentInChildren<Button>().interactable = false;
        }

        defaultSlotSpaces = new List<CraftingSlot>(slotSpaces);
        defaultResultSpace = new ResultSlot(resultSpace);
    }

    public void ChooseBlueprint(Blueprint_UI blueprintUI)
    {
        Blueprint blueprint = blueprintUI.blueprint.blueprint;
        resultSpace.slotImage.sprite = blueprint.result.item.itemSprite;
        resultSpace.slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        resultSpace.nameText.text = blueprint.result.amount + " " + blueprint.result.item.itemName;
        if (blueprint.result.item is Equipment)
        {
            Equipment[] CurrentEquiped = GameManager.Instance.InventoryManager.GetEquipment();
            Equipment resultItem = (Equipment)blueprint.result.item;
            float statchange;

            resultSpace.hpText.text = "HP: " + resultItem.healthModifier;
            statchange = resultItem.healthModifier - CurrentEquiped[(int)resultItem.equipmentType].healthModifier;
            resultSpace.hpText.text += " (" + (statchange >= 0.0f ? ("+" + statchange.ToString()) : statchange.ToString()) + ")";

            resultSpace.defenceText.text = "Def: " + resultItem.armorModifier;
            statchange = resultItem.armorModifier - CurrentEquiped[(int)resultItem.equipmentType].armorModifier;
            resultSpace.defenceText.text += " (" + (statchange >= 0.0f ? ("+" + statchange.ToString()) : statchange.ToString()) + ")";

            resultSpace.digText.text = "Dig: " + resultItem.digModifier;
            statchange = resultItem.digModifier - CurrentEquiped[(int)resultItem.equipmentType].digModifier;
            resultSpace.digText.text += " (" + (statchange >= 0.0f ? ("+" + statchange.ToString()) : statchange.ToString()) + ")";

            resultSpace.attackText.text = "Atk: " + resultItem.attackModifier;
            statchange = resultItem.attackModifier - CurrentEquiped[(int)resultItem.equipmentType].attackModifier;
            resultSpace.attackText.text += " (" + (statchange >= 0.0f ? ("+" + statchange.ToString()) : statchange.ToString()) + ")";
        }
        else
        {
            resultSpace.hpText.text = "";
            resultSpace.defenceText.text = "";
            resultSpace.digText.text = "";
            resultSpace.attackText.text = "";
        }
        

        foreach (CraftingSlot slot in slotSpaces)
        {
            if (blueprint.recipe.Count > slot.slotIndex)
            {
                slot.slotImage.sprite = blueprint.recipe[slot.slotIndex].item.itemSprite;
                slot.slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                slot.amountText.text = blueprint.recipe[slot.slotIndex].item.itemName + ": " + blueprint.recipe[slot.slotIndex].amount.ToString();
            }
            else
            {
                slot.slotImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                slot.amountText.text = "";
            }
        }

        currentBlueprint = blueprint;
    }

    private void OnDisable()
    {
        slotSpaces = defaultSlotSpaces;
        resultSpace = defaultResultSpace;
    }

    public void Craft()
    {
        if (GameManager.Instance.UIManager.onCraftingTable == true)
        {
            GameManager.Instance.InventoryManager.Craft(currentBlueprint);
        }
    }

    public class CraftingSlot
    {
        public CraftingSlot()
        {
        }

        public CraftingSlot(CraftingSlot slot)
        {
            slotImage = slot.slotImage;
            amountText = slot.amountText;
            slotIndex = slot.slotIndex;
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

    public class ResultSlot
    {
        public ResultSlot()
        {
        }

        public ResultSlot(ResultSlot slot)
        {
            slotImage = slot.slotImage;
            nameText = slot.nameText;
            hpText = slot.hpText;
            defenceText = slot.defenceText;
            digText = slot.digText;
            attackText = slot.attackText;
        }

        public ResultSlot(Image image, Text name, Text HP, Text defence, Text dig, Text attack)
        {
            slotImage = image;
            nameText = name;
            hpText = HP;
            defenceText = defence;
            digText = dig;
            attackText = attack;
        }

        public Image slotImage;
        public Text nameText;
        public Text hpText;
        public Text defenceText;
        public Text digText;
        public Text attackText;
    }
}
