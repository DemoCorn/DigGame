using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject menuPrefab;
    private sians_Inventory_screen menuScript;

    private Inputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        GameObject menus = Instantiate(menuPrefab);

        menuScript = menus.GetComponent<sians_Inventory_screen>();
        inputs = GameManager.Instance.GetInputs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputs.inventoryOpen))
        {
            menuScript.OpenCloseMenu(UIMenu.inventory);
        }
        else if (Input.GetKeyDown("q"))
        {
            menuScript.OpenCloseMenu(UIMenu.equipment);
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
