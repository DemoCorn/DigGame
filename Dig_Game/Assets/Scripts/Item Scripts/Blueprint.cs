using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Blueprint", menuName = "Items/Blueprint", order = 3)]
public class Blueprint : ScriptableObject
{
    public List<ItemGroup> recipe;
    public ItemGroup result;
}
