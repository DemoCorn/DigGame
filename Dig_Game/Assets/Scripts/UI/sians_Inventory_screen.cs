using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sians_Inventory_screen : MonoBehaviour
{
    public GameObject inventoryscreen;
    public GameObject mainscreen;
    public GameObject settingstab;
    public GameObject Equipmentscreen;
    // Start is called before the first frame update
    void Start()
    {
        mainscreen.SetActive(true);
        inventoryscreen.SetActive(false);
        settingstab.SetActive(false);
        Equipmentscreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenInventory()
    {
        mainscreen.SetActive(false);
        inventoryscreen.SetActive(true);
        settingstab.SetActive(false);
        Equipmentscreen.SetActive(false);
    }

    public void CloseMenu()
    {
        mainscreen.SetActive(true);
        inventoryscreen.SetActive(false);
        settingstab.SetActive(false);
        Equipmentscreen.SetActive(false);
    }

    public void SettingsOpen()
    {
        mainscreen.SetActive(false);
        inventoryscreen.SetActive(false);
        settingstab.SetActive(true);
        Equipmentscreen.SetActive(false);
    }
    
    public void EquipmentOpen()
    {
        mainscreen.SetActive(false);
        inventoryscreen.SetActive(false);
        settingstab.SetActive(false);
        Equipmentscreen.SetActive(true);
    }
}
