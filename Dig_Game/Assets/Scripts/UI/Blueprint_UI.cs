using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blueprint_UI : MonoBehaviour
{
    [HideInInspector] public UnlockableBlueprint blueprint;
    private Button button;

    public void Start()
    {
        button = GetComponentInChildren<Button>();
    }

    public void Update()
    {
        button.interactable = blueprint.isUnlocked;
    }
}
