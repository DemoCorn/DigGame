using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotifyScript : MonoBehaviour
{
    public GameObject itemNotifyIcon;
    public GameObject itemNotifyPanel;
    public Text blueprintNameText;

    private Animation iconAnimation;

    private void Start()
    {
        iconAnimation = GetComponent<Animation>();
        itemNotifyPanel = GameObject.FindWithTag("PickupPanel");
        blueprintNameText = itemNotifyPanel.GetComponentInChildren<Text>();
    }

    public void DisplayItemNotificationUI()
    {
        ShowIcon();     
    }

    public void ShowIcon() 
    { 
        itemNotifyIcon.SetActive(true);
        itemNotifyPanel.SetActive(true);
        iconAnimation = itemNotifyIcon.GetComponent<Animation>();
        iconAnimation.Play();
        Invoke("SetIconandPanelInactive", 1.0f);
    }

    public void SetIconandPanelInactive()
    {
        itemNotifyIcon.SetActive(false);
        itemNotifyPanel.SetActive(false);
    }
}
