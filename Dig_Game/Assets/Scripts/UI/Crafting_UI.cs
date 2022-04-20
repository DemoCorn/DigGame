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
    private CraftingSlot resultSpace;
    private CraftingSlot defaultResultSpace;

    private Blueprint currentBlueprint;

    public GameObject blueprintButton;
    public GameObject blueprintList;

    /*
    public Text itemNameText;
    public Text classReqText;
    public Text hpText;
    public Text defText;
    public Text atkText;
    public Text digText;
    */
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotObjects.Count; i++)
        {
            slotSpaces.Add(new CraftingSlot(slotObjects[i].GetComponentInChildren<Image>(), slotObjects[i].GetComponentInChildren<Text>(), i));
        }
        resultSpace = new CraftingSlot(resultObject.GetComponentInChildren<Image>(), resultObject.GetComponentInChildren<Text>(), 0);

        GameObject newButton;
        List<UnlockableBlueprint> blueprints = GameManager.Instance.InventoryManager.GetBlueprints();
        Blueprint_UI bpUI;

        newButton = blueprintButton;
        bpUI = newButton.GetComponent<Blueprint_UI>();
        bpUI.blueprint = blueprints[0];
        newButton.GetComponentInChildren<Text>().text = bpUI.blueprint.blueprint.result.item.itemName;

        for (int i = 1; i < blueprints.Count; i++)
        {
            newButton = Instantiate(blueprintButton, blueprintList.transform);
            bpUI = newButton.GetComponent<Blueprint_UI>();
            bpUI.blueprint = blueprints[i];
            newButton.GetComponentInChildren<Text>().text = bpUI.blueprint.blueprint.result.item.itemName;
            newButton.GetComponentInChildren<Button>().interactable = false;
        }

        defaultSlotSpaces = new List<CraftingSlot>(slotSpaces);
        defaultResultSpace = new CraftingSlot(resultSpace);
    }

    public void ChooseBlueprint(Blueprint_UI blueprintUI)
    {
        Blueprint blueprint = blueprintUI.blueprint.blueprint;
        resultSpace.slotImage.sprite = blueprint.result.item.itemSprite;
        resultSpace.slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        Debug.LogWarning("Crafting_UI is only using name for the text someone needs to figure out the formatting we want and then delete this warning from the code"); // Delete this line once resultSpace.amountText.text isn't just using blueprint.result.item.itemName
        resultSpace.amountText.text = blueprint.result.item.itemName;
        //itemNameText.text = blueprint.result.item.name;
        

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
        GameManager.Instance.InventoryManager.Craft(currentBlueprint);
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
}
