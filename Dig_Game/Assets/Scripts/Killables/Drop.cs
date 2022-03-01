using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] public List<DropTable> drops = new List<DropTable>();
    [SerializeField] public List<BlueprintTable> blueprintDrops = new List<BlueprintTable>();
    public bool dropBlueprints = false;

    void OnDisable()
    {
        float chance;
        chance = Random.Range(0.0f, 100.0f); // This should never come up, but putting in 0 for the chance will actually still give it a chance under this implementation
        // Iterate through all drops and generate a random number to see if they get added to the inventory
        if (dropBlueprints)
        {
            foreach (BlueprintTable blueprint in blueprintDrops)
            {
                if (chance <= blueprint.percentChance)
                {
                    GameManager.Instance.InventoryManager.AddBlueprint(blueprint.blueprint);
                }
                else
                {
                    chance -= blueprint.percentChance;
                }
            }
        }
        else
        {
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
}
