using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;

    // Only items can be added to the inventory, this is a simple way to determin what an item should be casted to within certain operations
    // Any child class of item should override this to be the appropriate ItemType
    protected const ItemType itemType = ItemType.item;
    public ItemType GetItemType()
    {
        return itemType;
    }
}
