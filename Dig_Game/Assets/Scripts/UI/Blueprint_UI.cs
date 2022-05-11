using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blueprint_UI : MonoBehaviour
{
    [HideInInspector] public UnlockableBlueprint blueprint;
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
        }
        else
        {
            button.interactable = false;
            text.text = "???";
        }
    }
}
