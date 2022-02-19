using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private List<DropTable> drops = new List<DropTable>();

    void OnDisable()
    {
        float chance;
        chance = Random.Range(0.0f, 100.0f); // This should never come up, but putting in 0 for the chance will actually still give it a chance under this implementation
        // Iterate through all drops and generate a random number to see if they get added to the inventory
        foreach (DropTable drop in drops)
        {
            if (chance <= drop.percentChance)
            {
                GameManager.Instance.EditInventory(drop.items);
            }
            else
            {
                chance -= drop.percentChance;
            }
        }
    }

    // Used to allow designers to edit the chance that an ItemGroup has to drop from any given enemy
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
