using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private List<DropTable> drops = new List<DropTable>();

    void OnDisable()
    {
        float chance;
        foreach(DropTable drop in drops)
        {
            chance = Random.Range(0.0f, 100.0f);
            if (chance <= drop.percentChance)
            {
                GameManager.Instance.EditInventory(drop.items);
            }
        }
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
}
