using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotifyMessenger : MonoBehaviour
{
    [SerializeField] private Image itemNotifyPanel = null;
    [SerializeField] private Image itemNotifyPanelImage = null;
    [SerializeField] private Text itemNotifyPanelText = null;

    [HideInInspector] public Image itemNotifyIcon = null;

    private Animation iconAnimation = null;

    public void ShowIcon(string message)
    {
        itemNotifyIcon.color = Color.white;
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
        GameManager.Instance.InventoryManager.itemNotifyScript.MessengerDelete();
        Destroy(gameObject);
    }
}
