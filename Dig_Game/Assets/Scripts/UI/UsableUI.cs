using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> usableSlot = new List<GameObject>();
    private List<UsableSpace> usableButtons = new List<UsableSpace>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < usableSlot.Count; i++)
        {
            usableButtons.Add(new UsableSpace(usableSlot[i].GetComponentInChildren<Image>(), usableSlot[i].GetComponentInChildren<Text>(), i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        UsableGroup[] usables = GameManager.Instance.InventoryManager.GetUsable();

        for (int i = 0; i < 3; i++)
        {
            if (usables[i].amount > 0)
            {
                usableButtons[i].slotImage.sprite = usables[i].usable.itemSprite;
                usableButtons[i].slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                usableButtons[i].amountText.text = usables[i].amount.ToString();
            }
            else
            {
                usableButtons[i].slotImage.sprite = null;
                usableButtons[i].slotImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                usableButtons[i].amountText.text = "";
            }
        }
    }

    public void Unequip(int SlotNum)
    {
        GameManager.Instance.InventoryManager.UnequipUsable(SlotNum);
    }

    public class UsableSpace
    {
        public UsableSpace()
        {
        }

        public UsableSpace(Image image, Text text, int index)
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
