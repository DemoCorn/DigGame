using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drop : MonoBehaviour
{
    [SerializeField] public ItemDropTable itemDrops;
    [SerializeField] public BlueprintDropTable blueprintDrops;

    [SerializeField] public List<ItemDropByLayer> itemDropByLevel = new List<ItemDropByLayer>();
    [SerializeField] public List<BlueprintDropByLayer> blueprintDropByLevel = new List<BlueprintDropByLayer>();

    public bool dropBlueprints = false;
    private bool heapDrop = false;
    public bool smartDrop = false;
    private float maxChance = 100.0f;

    //Item Notification
    private ItemNotifyScript itemNotifyScript;


    private void Start()
    {
        itemNotifyScript = GetComponent<ItemNotifyScript>();

        if (smartDrop)
        {
            SmartDropSetup();
        }

        if (dropBlueprints)
        {
            for (int i = 0; i < blueprintDrops.drops.Count; i++)
            {
                if (GameManager.Instance.InventoryManager.BlueprintUnlocked(blueprintDrops.drops[i].blueprint))
                {
                    blueprintDrops.drops.Remove(blueprintDrops.drops[i]);
                    i--;
                }
            }

            if (blueprintDrops.drops.Count == 0)
            {
                Destroy(gameObject);
            }

            heapDrop = blueprintDrops.heapDrop;
        }
        else
        {
            heapDrop = itemDrops.heapDrop;
        }

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
    }

    void OnDisable()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            float chance;
            chance = Random.Range(0.0f, maxChance); // This should never come up, but putting in 0 for the chance will actually still give it a chance under this implementation

            // Iterate through all drops and generate a random number to see if they get added to the inventory
            if (dropBlueprints)
            {
                if (blueprintDrops.drops.Count != 0)
                {
                    foreach (BlueprintTable blueprint in blueprintDrops.drops)
                    {
                        if (chance <= blueprint.percentChance)
                        {
                            GameManager.Instance.InventoryManager.AddBlueprint(blueprint.blueprint);
                            itemNotifyScript.DisplayItemNotificationUI();
                        }
                        else
                        {
                            chance -= blueprint.percentChance;
                        }
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

        //Notify player what they've picked up. 
    }

    void SmartDropSetup()
    {
        LevelRange levels = GameManager.Instance.LayerManager.GetLevelRange();
        if (dropBlueprints)
        {
            for (int i = 0; i < blueprintDropByLevel[levels.nLevelNumber].blueprintAtLayer.Count; i++)
            {
                if (gameObject.transform.position.y <= levels.layerRange[i].highest && gameObject.transform.position.y >= levels.layerRange[i].lowest)
                {
                    blueprintDrops = blueprintDropByLevel[levels.nLevelNumber].blueprintAtLayer[i];
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < itemDropByLevel[levels.nLevelNumber].itemAtLayer.Count; i++)
            {
                if (gameObject.transform.position.y <= levels.layerRange[i].highest && gameObject.transform.position.y >= levels.layerRange[i].lowest)
                {
                    itemDrops = itemDropByLevel[levels.nLevelNumber].itemAtLayer[i];
                    break;
                }
            }
        }
    }

    [System.Serializable]
    public class ItemDropByLayer
    {
        public ItemDropByLayer()
        {
        }

        public ItemDropByLayer(List<ItemDropTable> layers)
        {
            itemAtLayer = layers;
        }
        public List<ItemDropTable> itemAtLayer = new List<ItemDropTable>();
    }

    [System.Serializable]
    public class BlueprintDropByLayer
    {
        public BlueprintDropByLayer()
        {
        }

        public BlueprintDropByLayer(List<BlueprintDropTable> layers)
        {
            blueprintAtLayer = layers;
        }
        public List<BlueprintDropTable> blueprintAtLayer = new List<BlueprintDropTable>();
    }
}