using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotifyScript : MonoBehaviour
{
    [HideInInspector] public GameObject itemNotifyIcon = null;
    [HideInInspector] public GameObject itemNotifyPanel = null;
    [HideInInspector] public GameObject itemNotifyPanelImage = null;
    [HideInInspector] public GameObject itemNotifyPanelText = null;
    [HideInInspector] public Text blueprintNameText = null;

    private Animation iconAnimation = null;

    public void DisplayItemNotificationUI()
    {
        ShowIcon();     
    }

    public void ShowIcon() 
    { 
        if (iconAnimation == null)
        {
            iconAnimation = GetComponent<Animation>();
            itemNotifyPanel = GameObject.FindWithTag("PickupPanel");
            itemNotifyPanelImage = GameObject.FindWithTag("IPPImage");
            itemNotifyPanelText = GameObject.FindWithTag("IPPText");
            blueprintNameText = itemNotifyPanel.GetComponentInChildren<Text>();
        }

        itemNotifyIcon.GetComponent<Image>().color = Color.white;
        itemNotifyPanel.GetComponent<Image>().color = Color.white;
        itemNotifyPanelImage.GetComponent<Image>().color = Color.white;
        itemNotifyPanelText.GetComponent<Text>().color = Color.white;

        iconAnimation = itemNotifyIcon.GetComponent<Animation>();
        iconAnimation.Play();
        Debug.Log("Pickup On");
        Invoke("SetIconandPanelInactive", 2.5f);

        
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
