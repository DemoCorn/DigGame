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

    public HPCog healthCog;

    private UIMenu currentMenu = UIMenu.hud;
    // Start is called before the first frame update
    void Start()
    {
        mainscreen.SetActive(true);
        inventoryscreen.SetActive(false);
        settingstab.SetActive(false);
        Equipmentscreen.SetActive(false);
    }

    public void OpenCloseMenu(UIMenu menu)
    {
        if (menu == UIMenu.hud || currentMenu == menu)
        {
            CloseMenu();
            currentMenu = UIMenu.hud;
        }
        else if (menu == UIMenu.inventory)
        {
            EquipmentOpen();
            currentMenu = menu;
        }
        else if (menu == UIMenu.equipment)
        {
            OpenInventory();
            currentMenu = menu;
        }
        else if (menu == UIMenu.settings)
        {
            SettingsOpen();
            currentMenu = menu;
        }
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