using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Usable", menuName = "Items/Usable", order = 2)]
public class Usable : Item
{
    [SerializeField] public GameObject effect;
}
