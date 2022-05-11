using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject menuPrefab;
    private sians_Inventory_screen menuScript;

    private Inputs inputs;

    [HideInInspector] public bool onCraftingTable = false;

    // Start is called before the first frame update
    public void Start()
    {
        inputs = GameManager.Instance.GetInputs();
    }

    public void BootUp()
    {
        GameObject menus = Instantiate(menuPrefab);
        menuScript = menus.GetComponent<sians_Inventory_screen>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputs.inventoryOpen))
        {
            if (onCraftingTable)
            {
                menuScript.OpenCloseMenu(UIMenu.equipment);
            }
            else
            {
                menuScript.OpenCloseMenu(UIMenu.inventory);
            }
        }
    }
}

public enum UIMenu
{
    hud = 0,
    inventory = 1,
    equipment = 2,
    settings = 3
}
