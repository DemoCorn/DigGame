using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropTables", menuName = "DropTables/ItemDropTable", order = 2)]
public class ItemDropTable : ScriptableObject
{
    [SerializeField] public List<DropTable> drops = new List<DropTable>();
}

[System.Serializable]
public class DropTable
{
    public DropTable()
    {
    }

    public DropTable(ItemGroup key, int value)
    {
        items = key;
        percentChance = value;
    }

    public ItemGroup items;
    public float percentChance;
}
