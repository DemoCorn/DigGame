using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    //Indicator for usable/ore drop
    private GameObject indicatorPrefab;
    private GameObject indicatorSpritePrefab;
    private GameObject target;

    [SerializeField] AudioSource collectSFX;

    private void Start()
    {
        indicatorPrefab = (GameObject)Resources.Load("Item_Ore IndicatorParent");
        indicatorSpritePrefab = (GameObject)Resources.Load("IndicatorSpriteParent");
        target = GameObject.FindGameObjectWithTag("Indicator");
        

        itemNotifyScript = GetComponent<ItemNotifyScript>();

        if (smartDrop)
        {
            SmartDropSetup();
        }

        if (dropBlueprints)
        {
            heapDrop = blueprintDrops.heapDrop;
            CorrectBlueprint();

            if (blueprintDrops.drops.Count == 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            heapDrop = itemDrops.heapDrop;
        }

        if (heapDrop)
        {
            maxChance = 0.0f;
            if (!dropBlueprints)
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
                if (CorrectBlueprint())
                {
                    chance = Random.Range(0.0f, maxChance);
                }

                if (blueprintDrops.drops.Count != 0)
                {
                    foreach (BlueprintTable blueprint in blueprintDrops.drops)
                    {
                        if (chance <= blueprint.percentChance)
                        {
                            GameManager.Instance.InventoryManager.AddBlueprint(blueprint.blueprint);
                            break;
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
                        ShowIndicator((drop.items.item.itemName + " x1").ToString());
                        ShowIndicatorSprite(drop.items.item.itemSprite);
                        break;
                    }
                    else
                    {
                        chance -= drop.percentChance;
                    }
                }
            }
            if (collectSFX)
            {
                collectSFX.Play();
                Debug.Log("The sound has played");
            }

        }
    }

    bool CorrectBlueprint()
    {
        bool returnValue = false;
        for (int i = 0; i < blueprintDrops.drops.Count; i++)
        {
            if (GameManager.Instance.InventoryManager.BlueprintUnlocked(blueprintDrops.drops[i].blueprint))
            {
                blueprintDrops.drops.Remove(blueprintDrops.drops[i]);
                i--;
                returnValue = true && heapDrop;
            }
        }

        if (heapDrop && returnValue)
        {
            maxChance = 0.0f;
            if (dropBlueprints)
            {
                foreach (BlueprintTable blueprint in blueprintDrops.drops)
                {
                    maxChance += blueprint.percentChance;
                }
            }
        }
        return returnValue;
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

    void ShowIndicator(string text)
    {
        if (indicatorPrefab)
        {
            GameObject prefab = Instantiate(indicatorPrefab, target.transform.position + new Vector3(1, 0, 0), Quaternion.identity, target.transform);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;

        }
    }
     void ShowIndicatorSprite(Sprite itemSprite)
    {
        if(indicatorSpritePrefab)
        {
            GameObject Imageprefab = Instantiate(indicatorSpritePrefab, target.transform.position + new Vector3(-0.5f,0f,0), Quaternion.identity, target.transform);
            Imageprefab.GetComponentInChildren<SpriteRenderer>().sprite = itemSprite;
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