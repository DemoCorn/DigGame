using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotifyScript : MonoBehaviour
{
    public GameObject itemNotifyIcon = null;
    [HideInInspector] public GameObject itemNotifyPanel = null;
    [HideInInspector] public GameObject itemNotifyPanelImage = null;
    [HideInInspector] public GameObject itemNotifyPanelText = null;

    public GameObject ItemNotifyMessenger;

    private Animation iconAnimation = null;

    public void DisplayBlueprint(Blueprint bp)
    {
        //ShowIcon("Gained Blueprint: " + bp.result.item.itemName);
        GameObject messenger = GameManager.Instance.UIManager.AddToCanvas(ItemNotifyMessenger, new Vector3(506.0f, 362.0f, 90.0f));

        ItemNotifyMessenger messengerScript = messenger.GetComponent<ItemNotifyMessenger>();
        messengerScript.ShowIcon("Gained Blueprint: " + bp.result.item.itemName);
    }

    public void ShowIcon(string message) 
    { 
        if (iconAnimation == null)
        {
            iconAnimation = GetComponent<Animation>();
            itemNotifyPanel = GameObject.FindWithTag("PickupPanel");
            itemNotifyPanelImage = GameObject.FindWithTag("IPPImage");
            itemNotifyPanelText = GameObject.FindWithTag("IPPText");
        }

        itemNotifyIcon.GetComponent<Image>().color = Color.white;
        itemNotifyPanel.GetComponent<Image>().color = Color.white;
        itemNotifyPanelImage.GetComponent<Image>().color = Color.white;
        itemNotifyPanelText.GetComponent<Text>().text = message;
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

    public void MessengerDelete()
    {

    }
}
