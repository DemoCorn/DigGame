using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_Manager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject menuPrefab;
    private sians_Inventory_screen menuScript;

    private Inputs inputs;

    [HideInInspector] public bool onCraftingTable = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject menus = Instantiate(menuPrefab);

        menuScript = menus.GetComponent<sians_Inventory_screen>();
        inputs = GameManager.Instance.GetInputs();
        gameManager = GameManager.Instance;
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

        if (menuScript.mainscreen)
        {
            menuScript.healthCog.UpdateHealth(gameManager.GetPlayerHealth(), gameManager.GetPlayerMaxHealth());
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
