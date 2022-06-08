using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotifyScript : MonoBehaviour
{
    public GameObject itemNotifyIcon;
    public GameObject itemNotifyPanel;
    public GameObject itemNotifyPanelImage;
    public GameObject itemNotifyPanelText;
    public Text blueprintNameText;

    private Animation iconAnimation;

    private void Start()
    {
        iconAnimation = GetComponent<Animation>();
        itemNotifyPanel = GameObject.FindWithTag("PickupPanel");
        itemNotifyPanelImage = GameObject.FindWithTag("IPPImage");
        itemNotifyPanelText = GameObject.FindWithTag("IPPText");
        blueprintNameText = itemNotifyPanel.GetComponentInChildren<Text>();
    }

    public void DisplayItemNotificationUI()
    {
        ShowIcon();     
    }

    public void ShowIcon() 
    { 
        itemNotifyIcon.GetComponent<Image>().color = Color.white;
        itemNotifyPanel.GetComponent<Image>().color = Color.white;
        itemNotifyPanelImage.GetComponent<Image>().color = Color.white;
        itemNotifyPanelText.GetComponent<Text>().color = Color.white;

        iconAnimation = itemNotifyIcon.GetComponent<Animation>();
        iconAnimation.Play();
        Debug.Log("Pickup On");
        Invoke("SetIconandPanelInactive", 1.0f);

        
    }

    public void SetIconandPanelInactive()
    {
        itemNotifyIcon.GetComponent<Image>().color = Color.clear;
        itemNotifyPanel.GetComponent<Image>().color = Color.clear;
        itemNotifyPanelImage.GetComponent<Image>().color = Color.clear;
        itemNotifyPanelText.GetComponent<Text>().color = Color.clear;
        Debug.Log("Pickup Off");
    }
}
