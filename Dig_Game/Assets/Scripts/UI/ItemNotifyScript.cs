using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class ItemNotifyScript : MonoBehaviour
{
    public GameObject ItemNotifyMessenger;
    private List<int> usedMessengerSlots = new List<int>();

    public void DisplayBlueprint(Blueprint bp)
    {
        int nID = -1;
        for (int i = 0; i < 20; i++)
        {
            if (usedMessengerSlots.IndexOf(i) == -1)
            {
                nID = i;
                usedMessengerSlots.Add(nID);
                break;
            }
        }
        Assert.AreNotEqual(nID, -1, "Too many notifications triggered at once");

        GameObject messenger = GameManager.Instance.UIManager.AddToCanvas(ItemNotifyMessenger, new Vector3(506.0f, 362.0f - (150.0f * nID), 90.0f));

        ItemNotifyMessenger messengerScript = messenger.GetComponent<ItemNotifyMessenger>();
        messengerScript.nID = nID;
        messengerScript.ShowIcon("Gained Blueprint: " + bp.result.item.itemName);
    }

    public void MessengerDelete(int nID)
    {
        usedMessengerSlots.Remove(nID);
    }
}
