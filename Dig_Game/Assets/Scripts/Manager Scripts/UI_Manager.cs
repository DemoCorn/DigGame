using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.Assertions;


public class UI_Manager : MonoBehaviour
{
    [HideInInspector] public bool loaded = false;
    [SerializeField] private GameObject menuPrefab;
    private GameObject menus;
    private sians_Inventory_screen menuScript;
    //private HPCog cog;
    private HPBar bar;

    private  List<Transform> compassLocations = new List<Transform>();
    private GameObject compassCenter;

    private GameObject retireScreen;

    private Inputs inputs;
    private UnityEvent UILoaded;

    [SerializeField] private GameObject ItemNotifyMessenger;
    [SerializeField] private float NotificationSpacing = 150.0f;
    private List<int> usedMessengerSlots = new List<int>();

    [HideInInspector] public bool onCraftingTable = false;

    // Start is called before the first frame update
    public void Start()
    {
        inputs = GameManager.Instance.GetInputs();
    }

    public void BootUp()
    {
        menus = Instantiate(menuPrefab);
        loaded = true;

        UIEssentials ui = menus.GetComponent<UIEssentials>();

        menuScript = menus.GetComponent<sians_Inventory_screen>();
        compassCenter = ui.CompassCenter;
        retireScreen = ui.RetireConfirm;
        compassLocations.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isMainLevel)
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
            //cog.UpdateHealth(GameManager.Instance.GetPlayerHealth(), GameManager.Instance.GetPlayerMaxHealth());
            bar.UpdateHealth(GameManager.Instance.GetPlayerHealth(), GameManager.Instance.GetPlayerMaxHealth());
            DrawClosestPoint();
        }
    }

    private void DrawClosestPoint()
    {
        Vector3 playerPos = GameManager.Instance.GetPlayerPosition();
        float closestDistance = float.MaxValue;
        int closestIndex = -1;
        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

        for (int i = 0; i < compassLocations.Count; ++i)
        {
            float distance = Vector3.Distance(playerPos, transform.TransformPoint(compassLocations[i].position));
            Mathf.Abs(distance);
            if (distance < closestDistance)
            {
                closestIndex = i;
                closestDistance = distance;
                position = compassLocations[closestIndex].position;
            }
        }

        if (closestIndex != -1)
        {
            //compassCenter.transform.eulerAngles = new Vector3(0.0f, 0.0f, Angle360(playerPos, position));
        }
    }

    public void SetRetireScreen()
    {
        retireScreen.SetActive(!retireScreen.activeSelf);
    }

    private float Angle360(Vector2 p1, Vector2 p2)
    {
        double xDiff = p2.x - p1.x;
        double yDiff = p2.y - p1.y;
        return (float)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);
    }

    public void SetCog(HPCog setCog)
    {
        //cog = setCog;
    }

    public void SetBar(HPBar setBar)
    {
        bar = setBar;
    }

    public void RegisterCompassLocation(Transform pos)
    {
        compassLocations.Add(pos);
    }

    public GameObject AddToCanvas(GameObject gameObject, Vector3 position)
    {
        Transform createdTransform = Instantiate(gameObject).transform;
        GameObject createdObject = createdTransform.gameObject;

        createdTransform.parent = menus.transform;

        createdTransform.localPosition = position;

        return createdObject;
    }

    public void DisplayBlueprint(Blueprint bp)
    {
        MessengerCreation("Gained Blueprint: " + bp.result.item.itemName);
    }

    private void MessengerCreation(string message)
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

        GameObject messenger = GameManager.Instance.UIManager.AddToCanvas(ItemNotifyMessenger, new Vector3(506.0f, 362.0f - (NotificationSpacing * nID), 90.0f));

        ItemNotifyMessenger messengerScript = messenger.GetComponent<ItemNotifyMessenger>();
        messengerScript.nID = nID;
        messengerScript.ShowIcon(message);
    }

    public void MessengerDelete(int nID)
    {
        usedMessengerSlots.Remove(nID);
    }
}

public enum UIMenu
{
    hud = 0,
    inventory = 1,
    equipment = 2,
    settings = 3
}
