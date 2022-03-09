using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropTables", menuName = "DropTables/BlueprintDropTable", order = 1)]
public class BlueprintDropTable : ScriptableObject
{
    [SerializeField] public List<BlueprintTable> drops = new List<BlueprintTable>();
}

[System.Serializable]
public class BlueprintTable
{
    public BlueprintTable()
    {
    }

    public BlueprintTable(Blueprint key, int value)
    {
        blueprint = key;
        percentChance = value;
    }

    public Blueprint blueprint;
    public float percentChance;
}
