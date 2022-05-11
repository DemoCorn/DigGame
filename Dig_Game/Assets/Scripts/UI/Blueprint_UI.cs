using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Blueprint_UI : MonoBehaviour
{
    [HideInInspector] public UnlockableBlueprint blueprint;
    public int index;
    private Button button;
    private Text text;

    public void Start()
    {
        button = GetComponentInChildren<Button>();
        text = GetComponentInChildren<Text>();
    }

    public void Update()
    {
        if (blueprint.isUnlocked)
        {
            button.interactable = true;
            text.text = blueprint.blueprint.result.item.itemName;
            transform.SetSiblingIndex(0);
        }
        else
        {
            button.interactable = false;
            text.text = "???";
            transform.SetSiblingIndex(GameManager.Instance.InventoryManager.GetBlueprints().Count + 100);
        }
    }
}
