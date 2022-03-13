using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation_Manager : MonoBehaviour
{
    public int levelWidth = 60;

    
    public GameObject dirtObj;
    
    [Header("Small generation prefabs")]
    [Tooltip("Ore that will spawn in a given level")]
    public List<LevelOre> oreLevelRanges;
    [Tooltip("Scatter objects that will spawn in a given level")]
    public List<LevelScatter> levelScatters;

    [Header("Large generation prefabs")]
    [Tooltip("Room prefabs that will spawn in a given level")]
    public List<LevelPrefab> puzzlePrefabs;
    [Tooltip("Room prefabs that will spawn in a given level")]
    public List<LevelPrefab> treasurePrefabs;

    [Header("Crash Protection")]
    public int maxRoomSpawnAttempts = 500;

    [Header("Misc")]
    [SerializeField] private GameObject craftingTable;

    private GetPrefabSize _prefabSizeScript;
    private HashSet<KeyValuePair<int, int>> reservedSpaces = new HashSet<KeyValuePair<int, int>>();


    // Start is called before the first frame update
    public void Start()
    {
        // Temporary crafting table spawn just to have it
        Instantiate(craftingTable, new Vector3(30.0f, 1.0f, 0.0f), Quaternion.identity);

        // local variable set up
        int mineralX;
        int mineralY;
        int oreAmount;

        int scatterX;
        int scatterY;
        int scatterAmount;

        LevelRange levels = GameManager.Instance.LayerManager.GetLevelRange();
        List<ScatterRange> scatterRanges = levelScatters[levels.nLevelNumber].scatterAtLevel;
        List<OreRange> oreRanges = oreLevelRanges[levels.nLevelNumber].oreAtLevel;

        // loop to generate layers
        for (int i = 0; i < levels.layerRange.Count; i++)
        {
            // Generate large prefabs
            GenerateRooms(puzzlePrefabs[levels.nLevelNumber].prefabAtLevel[i], levels, i);
            GenerateRooms(treasurePrefabs[levels.nLevelNumber].prefabAtLevel[i], levels, i);

            // Generate scatter objects
            foreach (ScatterType scatterType in scatterRanges[i].scatterAtLayer)
            {
                scatterAmount = Random.Range(scatterType.lowestScatterAmount, scatterType.highestScatterAmount + 1);
                for (int j = 0; j < scatterAmount; j++)
                {
                    scatterX = Random.Range(0, levelWidth);
                    scatterY = Random.Range((int)levels.layerRange[i].lowest, (int)levels.layerRange[i].highest + 1);

                    if (!CheckReserved(scatterX, scatterY))
                    {
                        ReserveSpace(scatterX, scatterY);
                        Instantiate(scatterType.scatterObject, new Vector2(scatterX, scatterY), scatterType.scatterObject.transform.rotation);
                        GenerateScatter(scatterType, scatterX, scatterY);
                    }
                    else
                    {
                        j -= 1;
                        continue;
                    }
                }
            }

            // Generate Minerals
            foreach (OreType ore in oreRanges[i].oreAtLayer)
            {
                // Random Minerals
                oreAmount = Random.Range(ore.lowestOreAmount, ore.highestOreAmount + 1);

                for (int j = 0; j < oreAmount; j++)
                {
                    mineralX = Random.Range(0, levelWidth);
                    mineralY = Random.Range((int) levels.layerRange[i].lowest, (int) levels.layerRange[i].highest + 1);

                    if (!CheckReserved(mineralX, mineralY))
                    {
                        ReserveSpace(mineralX, mineralY);
                        Instantiate(ore.ore, new Vector2(mineralX, mineralY), ore.ore.transform.rotation);
                    }
                    else
                    {
                        j -= 1;
                        continue;
                    }
                }
            }

            // Generate dirt blocks
            for (float xPos = 0f; xPos < levelWidth; xPos++)
            {
                for (float yPos = levels.layerRange[i].lowest; yPos <= levels.layerRange[i].highest; yPos++)
                {
                    if (!CheckReserved((int) xPos, (int) yPos))
                    {
                        Instantiate(dirtObj, new Vector2(xPos, yPos), dirtObj.transform.rotation);
                    }
                }
            }
        }

    }

    // Generates prefabs larger than 1x1
    private void GenerateRooms(PrefabRange prefabRange, LevelRange levels, int layer)
    {
        // Local Variable set up
        float prefabPosX;
        float prefabPosY;
        KeyValuePair<float, float> height = GameManager.Instance.LayerManager.GetLevelHeight();

        int randomRoom = 0;
        int prefabAmount = Random.Range(prefabRange.lowestPrefabAmount, prefabRange.highestPrefabAmount + 1);

        bool reserved = false;

        //Generation start
        for (int i = 0; i < prefabAmount; i++)
        {
            // Choose a random prefab
            float prefabPercent = Random.Range(0.0f, 100.0f);
            for (int j = 0; j < prefabRange.prefabAtLayer.Count; j++)
            {
                if (prefabPercent <= prefabRange.prefabAtLayer[j].chance)
                {
                    randomRoom = j;
                    _prefabSizeScript = prefabRange.prefabAtLayer[randomRoom].prefab.GetComponent<GetPrefabSize>();
                    Debug.Log("Generated prefab: " + randomRoom + " from list");
                    break;
                }
                else
                {
                    prefabPercent -= prefabRange.prefabAtLayer[j].chance;
                }
            }

            // try to place the level
            int runTimes = 0;
            do
            {
                // Crash protection, if a place for the level can't be found the loop will eventually stop
                runTimes++;
                if (runTimes >= maxRoomSpawnAttempts)
                {
                    Debug.LogWarning("Generation Manager was unable to instantiate " + prefabRange.prefabAtLayer[randomRoom].prefab.name);
                    break;
                }

                // Choose a position to spawn the prefab
                reserved = false;
                prefabPosX = Random.Range(0, levelWidth);
                prefabPosY = Random.Range((int)levels.layerRange[layer].lowest, (int)levels.layerRange[layer].highest);

                // Make sure the prefab will not generate outside of the level bounds
                // Check for overly high x
                if (prefabPosX + _prefabSizeScript.highestX + _prefabSizeScript.xOffset >= levelWidth)
                {
                    prefabPosX -= (prefabPosX + _prefabSizeScript.highestX) - (levelWidth - 1);
                }
                // Check for overly low x
                else if (prefabPosX + _prefabSizeScript.lowestX + _prefabSizeScript.xOffset < 0)
                {
                    prefabPosX -= (prefabPosX + _prefabSizeScript.lowestX);
                }

                // Check for overly high y
                if (prefabPosY + _prefabSizeScript.highestY + _prefabSizeScript.yOffset > height.Value)
                {
                    prefabPosY -= (prefabPosY + _prefabSizeScript.highestY) - (height.Value);
                }
                // Check for overly low y
                else if (prefabPosY + _prefabSizeScript.lowestY + _prefabSizeScript.yOffset < height.Key)
                {
                    prefabPosY -= (prefabPosY + _prefabSizeScript.lowestY) - (height.Key);
                }

                // Check if any spot the prefab would generate on is already reserved
                for (int x = (int)(_prefabSizeScript.lowestX + _prefabSizeScript.xOffset); x <= (int)(_prefabSizeScript.highestX + _prefabSizeScript.xOffset); x++)
                {
                    for (int y = (int)(_prefabSizeScript.lowestY + _prefabSizeScript.yOffset); y <= (_prefabSizeScript.highestY + _prefabSizeScript.yOffset); y++)
                    {
                        if (CheckReserved((int)prefabPosX + x, (int)prefabPosY + y))
                        {
                            reserved = true;
                            break;
                        }

                    }
                    if (reserved)
                    {
                        break;
                    }
                }

                // Reserve all spaces the prefab is now on
                if (!reserved)
                {
                    for (int x = (int)(_prefabSizeScript.lowestX + _prefabSizeScript.xOffset); x <= (int)(_prefabSizeScript.highestX + _prefabSizeScript.xOffset); x++)
                    {
                        for (int y = (int)(_prefabSizeScript.lowestY + _prefabSizeScript.yOffset); y <= (_prefabSizeScript.highestY + _prefabSizeScript.yOffset); y++)
                        {
                            ReserveSpace((int)prefabPosX + x, (int)prefabPosY + y);
                        }
                    }
                    Instantiate(prefabRange.prefabAtLayer[randomRoom].prefab, new Vector3(prefabPosX, prefabPosY, 0), prefabRange.prefabAtLayer[randomRoom].prefab.transform.rotation);
                }
            } while (reserved);
        }
    }

    private void GenerateScatter(ScatterType scatter, int xPosition, int yPosition, int iteration = 0)
    {
        // Local variable set up
        KeyValuePair<float, float> height = GameManager.Instance.LayerManager.GetLevelHeight();

        float roll = Random.Range(0.0f, 100.0f);
        float chance = scatter.scatterChance;

        // lower chance based on how many times GenerateScatter has been called
        for (int i = 0; i < iteration; i++)
        {
            chance -= chance * (scatter.scatterChanceDegredation / 100.0f);
        }

        // Try to generate in 4 directions
        if (roll <= chance)
        {
            // +1 X
            if (!CheckReserved(xPosition + 1, yPosition) && xPosition + 1 < levelWidth && xPosition + 1 >= 0)
            {
                ReserveSpace(xPosition + 1, yPosition);
                Instantiate(scatter.scatterObject, new Vector2(xPosition + 1, yPosition), scatter.scatterObject.transform.rotation);
                GenerateScatter(scatter, xPosition + 1, yPosition, iteration + 1);
            }

            // -1 X
            if (!CheckReserved(xPosition - 1, yPosition) && xPosition - 1 < levelWidth && xPosition - 1 >= 0)
            {
                ReserveSpace(xPosition - 1, yPosition);
                Instantiate(scatter.scatterObject, new Vector2(xPosition - 1, yPosition), scatter.scatterObject.transform.rotation);
                GenerateScatter(scatter, xPosition - 1, yPosition, iteration + 1);
            }

            // +1 Y
            if (!CheckReserved(xPosition, yPosition + 1) && yPosition + 1 < height.Value && yPosition + 1 >= height.Key)
            {
                ReserveSpace(xPosition, yPosition + 1);
                Instantiate(scatter.scatterObject, new Vector2(xPosition, yPosition + 1), scatter.scatterObject.transform.rotation);
                GenerateScatter(scatter, xPosition, yPosition + 1, iteration + 1);
            }

            // -1 Y
            if (!CheckReserved(xPosition, yPosition - 1) && yPosition - 1 < height.Value && yPosition - 1 >= height.Key)
            {
                ReserveSpace(xPosition, yPosition - 1);
                Instantiate(scatter.scatterObject, new Vector2(xPosition, yPosition - 1), scatter.scatterObject.transform.rotation);
                GenerateScatter(scatter, xPosition, yPosition - 1, iteration + 1);
            }
        }
    }

    private bool ReserveSpace(int x, int y)
    {
        return reservedSpaces.Add(new KeyValuePair<int, int>(x, y));
    }
    
    private bool CheckReserved(int x, int y)
    {
        return reservedSpaces.Contains(new KeyValuePair<int, int>(x, y));
    }
    // Ore Classes --------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class LevelOre
    {
        public LevelOre()
        {
        }

        public LevelOre(List<OreRange> oreLayer)
        {
            oreAtLevel = oreLayer;
        }
        [Tooltip("Ore that will spawn in a given Layer")]
        public List<OreRange> oreAtLevel = new List<OreRange>();
    }

    [System.Serializable]
    public class OreRange
    {
        public OreRange()
        {
        }

        public OreRange(List<OreType> oreLayer, int nLow, int nHigh)
        {
            oreAtLayer = oreLayer;
        }
        [Tooltip("List of all ores that will spawn")]
        public List<OreType> oreAtLayer = new List<OreType>();
    }

    [System.Serializable]
    public class OreType
    {
        public OreType()
        {

        }

        public OreType(GameObject newOre, int nLow, int nHigh)
        {
            ore = newOre;
            lowestOreAmount = nLow;
            highestOreAmount = nHigh;
        }

        public GameObject ore;
        public int lowestOreAmount;
        public int highestOreAmount;
    }

    // Level Classes --------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class LevelPrefab
    {
        public LevelPrefab()
        {
        }

        public LevelPrefab(List<PrefabRange> layers)
        {
            prefabAtLevel = layers;
        }
        [Tooltip("Room prefab that will spawn in a given Layer")]
        public List<PrefabRange> prefabAtLevel = new List<PrefabRange>();
    }

    [System.Serializable]
    public class PrefabRange
    {
        public PrefabRange()
        {
        }

        public PrefabRange(List<PrefabType> prefabs, int nLow, int nHigh)
        {
            prefabAtLayer = prefabs;
            lowestPrefabAmount = nLow;
            highestPrefabAmount = nHigh;
        }
        [Tooltip("List of all room prefabs that will spawn")]
        public List<PrefabType> prefabAtLayer = new List<PrefabType>();
        public int lowestPrefabAmount;
        public int highestPrefabAmount;
    }

    [System.Serializable]
    public class PrefabType
    {
        public PrefabType()
        {
        }

        public PrefabType(GameObject newPrefab)
        {
            prefab = newPrefab;
        }
        public GameObject prefab;
        public float chance;
    }

    // Scatter Classes --------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class LevelScatter
    {
        public LevelScatter()
        {
        }

        public LevelScatter(List<ScatterRange> layers)
        {
            scatterAtLevel = layers;
        }
        [Tooltip("Scatter objects that will spawn in a given Layer")]
        public List<ScatterRange> scatterAtLevel = new List<ScatterRange>();
    }

    [System.Serializable]
    public class ScatterRange
    {
        public ScatterRange()
        {
        }

        public ScatterRange(List<ScatterType> scatters)
        {
            scatterAtLayer = scatters;
        }
        [Tooltip("List of all scatter objects that will spawn")]
        public List<ScatterType> scatterAtLayer = new List<ScatterType>();
    }

    [System.Serializable]
    public class ScatterType
    {
        public ScatterType()
        {
        }

        public ScatterType(GameObject newScatter, int nLow, int nHigh)
        {
            scatterObject = newScatter;
            lowestScatterAmount = nLow;
            highestScatterAmount = nHigh;
        }
        public GameObject scatterObject;
        public float scatterChance;
        public float scatterChanceDegredation;
        public int lowestScatterAmount;
        public int highestScatterAmount;
    }
}