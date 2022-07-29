using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> usableSlot = new List<GameObject>();
    private List<UsableSpace> usableButtons = new List<UsableSpace>();
    UsableGroup[] usables;
    float[] usableCooldowns = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        usables = GameManager.Instance.InventoryManager.GetUsable();
        for (int i = 0; i < usableSlot.Count; i++)
        {
            UsableEffect test = usables[i].usable.effect.GetComponent<UsableEffect>();
            float test2 = test.cooldownTime;
            usableCooldowns[i] = 0.0f;
            usableButtons.Add(new UsableSpace(usableSlot[i].GetComponentInChildren<Image>(), 
                                              usableSlot[i].GetComponentInChildren<Text>(), 
                                              usableSlot[i].GetComponent<Slider>(), 
                                              usables[i].usable.effect.GetComponent<UsableEffect>().cooldownTime, 
                                              i));
        }
        for (int i = 0; i < 3; i++)
        {
            usableButtons[i].slotImage.sprite = usables[i].usable.itemSprite;
            usableButtons[i].slotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            usableButtons[i].amountText.text = usables[i].amount.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            usableButtons[i].amountText.text = usables[i].amount.ToString();
            if (usables[i].cooldown && usableCooldowns[i] == 0.0f)
            {
                usableCooldowns[i] = usableButtons[i].coolDown;
                usableButtons[i].slider.value = 1;
            }
            else if (usableCooldowns[i] > 0.0f)
            {
                usableCooldowns[i] -= Time.deltaTime;

                usableButtons[i].slider.value = usableCooldowns[i] / usableButtons[i].coolDown;
                if (usableCooldowns[i] < 0.0f)
                {
                    usableCooldowns[i] = 0.0f;
                }
            }
            else
            {
                usableButtons[i].slider.value = 0;
            }
        }
    }

    public class UsableSpace
    {
        public UsableSpace()
        {
        }

        public UsableSpace(Image image, Text text, Slider slide, float cd, int index)
        {
            slotImage = image;
            amountText = text;
            slider = slide;
            coolDown = cd;
            buttonIndex = index;
        }

        public Image slotImage;
        public Text amountText;
        public Slider slider;
        public float coolDown;
        public int buttonIndex;
    }
}
