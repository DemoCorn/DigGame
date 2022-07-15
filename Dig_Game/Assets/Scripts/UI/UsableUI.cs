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
            usableButtons[i].slotImage.sprite = usables[i].usable.itemSprite;
            usableButtons[i].slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            usableButtons[i].amountText.text = usables[i].amount.ToString();
        }
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
