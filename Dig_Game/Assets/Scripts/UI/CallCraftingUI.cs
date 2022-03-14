using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCraftingUI : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.UIManager.onCraftingTable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.UIManager.onCraftingTable = false;
        }
    }
}
