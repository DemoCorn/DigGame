using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


public class UI_Manager : MonoBehaviour
{
    [HideInInspector] public bool loaded = false;
    [SerializeField] private GameObject menuPrefab;
    private sians_Inventory_screen menuScript;
    private HPCog cog;
    private  List<Transform> compassLocations = new List<Transform>();
    private GameObject compassCenter;

    private Inputs inputs;

    private UnityEvent UILoaded;

    [HideInInspector] public bool onCraftingTable = false;

    // Start is called before the first frame update
    public void Start()
    {
        inputs = GameManager.Instance.GetInputs();
    }

    public void BootUp()
    {
        GameObject menus = Instantiate(menuPrefab);
        loaded = true;

        menuScript = menus.GetComponent<sians_Inventory_screen>();
        compassCenter = menus.GetComponent<UIEssentials>().CompassCenter;
        compassLocations.Clear();
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
        cog.UpdateHealth(GameManager.Instance.GetPlayerHealth(), GameManager.Instance.GetPlayerMaxHealth());
        DrawClosestPoint();
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
            compassCenter.transform.eulerAngles = new Vector3(0.0f, 0.0f, Angle360(playerPos, position));
        }
    }

    private float Angle360(Vector2 p1, Vector2 p2)
    {
        double xDiff = p2.x - p1.x;
        double yDiff = p2.y - p1.y;
        return (float)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);
    }

    public void SetCog(HPCog setCog)
    {
        cog = setCog;
    }

    public void RegisterCompassLocation(Transform pos)
    {
        compassLocations.Add(pos);
    }
}

public enum UIMenu
{
    hud = 0,
    inventory = 1,
    equipment = 2,
    settings = 3
}
