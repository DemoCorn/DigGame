using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] public ItemDropTable itemDrops;
    [SerializeField] public BlueprintDropTable blueprintDrops;
    public bool dropBlueprints = false;
    public bool heapDrop = false;
    private float maxChance = 100.0f;

    private void Start()
    {
       if (heapDrop)
       {
            maxChance = 0.0f;
            if (dropBlueprints)
            {
                foreach (BlueprintTable blueprint in blueprintDrops.drops)
                {
                    maxChance += blueprint.percentChance;
                }
            }
            else
            {
                foreach (DropTable drop in itemDrops.drops)
                {
                    maxChance += drop.percentChance;
                }
            }
       }

       if (dropBlueprints)
       {
            for(int i = 0; i < blueprintDrops.drops.Count; i++)
            {
                if (GameManager.Instance.InventoryManager.BlueprintUnlocked(blueprintDrops.drops[i].blueprint))
                {
                    blueprintDrops.drops.Remove(blueprintDrops.drops[i]);
                    i--;
                }
            }
       }
    }

    void OnDisable()
    {
        float chance;
        chance = Random.Range(0.0f, maxChance); // This should never come up, but putting in 0 for the chance will actually still give it a chance under this implementation

        // Iterate through all drops and generate a random number to see if they get added to the inventory
        if (dropBlueprints)
        {
            foreach (BlueprintTable blueprint in blueprintDrops.drops)
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
            foreach (DropTable drop in itemDrops.drops)
            {
                if (chance <= drop.percentChance)
                {
                    GameManager.Instance.InventoryManager.EditInventory(drop.items);
                }
                else
                {
                    chance -= drop.percentChance;
                }
            }
        }
    }
}
