using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sians_Inventory_screen : MonoBehaviour
{
    public GameObject inventoryscreen;
    public GameObject mainscreen;
    // Start is called before the first frame update
    void Start()
    {
        mainscreen.SetActive(true);
        inventoryscreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInventory()
    {
        mainscreen.SetActive(false);
        inventoryscreen.SetActive(true);
    }

    public void CloseInventory()
    {
        mainscreen.SetActive(true);
        inventoryscreen.SetActive(false);
    }
}
