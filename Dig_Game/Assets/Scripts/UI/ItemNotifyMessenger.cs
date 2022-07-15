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

    [HideInInspector] public int nID;

    public void ShowIcon(string message)
    {
        itemNotifyIcon.color = Color.white;
        itemNotifyPanel.GetComponent<Image>().color = Color.white;
        itemNotifyPanelImage.GetComponent<Image>().color = Color.white;
        itemNotifyPanelText.GetComponent<Text>().text = message;
        itemNotifyPanelText.GetComponent<Text>().color = Color.white;

        Invoke("SetIconandPanelInactive", 2.5f);
    }

    public void SetIconandPanelInactive()
    {
        GameManager.Instance.UIManager.MessengerDelete(nID);
        Destroy(gameObject);
    }
}
