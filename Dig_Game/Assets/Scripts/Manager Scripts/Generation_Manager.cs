using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation_Manager : MonoBehaviour
{
    public int levelWidth = 60;

    // Attach the dirt tile objects in Unity to these Transforms
    public GameObject dirtObj;
    public List<LevelOre> oreLevelRanges;
    public List<LevelScatter> levelScatters;

    public List<LevelPrefab> puzzlePrefabs;
    public List<LevelPrefab> treasurePrefabs;

    public int maxRoomSpawnAttempts = 500;

    public static float prefabPosX;
    public static float prefabPosY;

    private GetPrefabSize _prefabSizeScript;

    private HashSet<KeyValuePair<int, int>> reservedSpaces = new HashSet<KeyValuePair<int, int>>();

    [SerializeField] private GameObject craftingTable;

    // Start is called before the first frame update
    public void Start()
    {
        Instantiate(craftingTable, new Vector3(30.0f, 1.0f, 0.0f), Quaternion.identity);

        int mineralX;
        int mineralY;
        int oreAmount;

        int scatterX;
        int scatterY;
        int scatterAmount;

        LevelRange levels = GameManager.Instance.LayerManager.GetLevelRange();
        List<ScatterRange> scatterRanges = levelScatters[levels.nLevelNumber].scatterAtLevel;
        List<OreRange> oreRanges = oreLevelRanges[levels.nLevelNumber].oreAtLevel;
        for (int i = 0; i < levels.layerRange.Count; i++)
        {
            GenerateRooms(puzzlePrefabs[levels.nLevelNumber].prefabAtLevel[i], levels, i);
            GenerateRooms(treasurePrefabs[levels.nLevelNumber].prefabAtLevel[i], levels, i);

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

            // Instantiate Dirt Blocks
            for (float xPos = 0f; xPos < levelWidth; xPos++)
            {
                for (float yPos = levels.layerRange[i].lowest; yPos <= levels.layerRange[i].highest; yPos++)
                {
                    // Place around the prefab
                    if (!CheckReserved((int) xPos, (int) yPos))
                    {
                        Instantiate(dirtObj, new Vector2(xPos, yPos), dirtObj.transform.rotation);
                    }
                }
            }
        }

    }
    private void GenerateRooms(PrefabRange prefabRange, LevelRange levels, int layer)
    {
        int randomRoom = 0;
        KeyValuePair<float, float> height = GameManager.Instance.LayerManager.GetLevelHeight();
        bool reserved = false;
        int prefabAmount = Random.Range(prefabRange.lowestPrefabAmount, prefabRange.highestPrefabAmount + 1);
        for (int j = 0; j < prefabAmount; j++)
        {
            float prefabPercent = Random.Range(0.0f, 100.0f);
            for (int k = 0; k < prefabRange.prefabAtLayer.Count; k++)
            {
                if (prefabPercent <= prefabRange.prefabAtLayer[k].chance)
                {
                    randomRoom = k;
                    _prefabSizeScript = prefabRange.prefabAtLayer[randomRoom].prefab.GetComponent<GetPrefabSize>();
                    Debug.Log("Generated prefab: " + randomRoom + " from list");
                    break;
                }
                else
                {
                    prefabPercent -= prefabRange.prefabAtLayer[k].chance;
                }
            }
            int runTimes = 0;
            do
            {
                runTimes++;
                if (runTimes >= maxRoomSpawnAttempts)
                {
                    Debug.LogWarning("Generation Manager was unable to instantiate " + prefabRange.prefabAtLayer[randomRoom].prefab.name);
                    break;
                }

                reserved = false;
                prefabPosX = Random.Range(0, levelWidth);
                prefabPosY = Random.Range((int)levels.layerRange[layer].lowest, (int)levels.layerRange[layer].highest);

                if (prefabPosX + _prefabSizeScript.highestX + _prefabSizeScript.xOffset >= levelWidth)
                {
                    prefabPosX -= (prefabPosX + _prefabSizeScript.highestX) - (levelWidth - 1);
                }
                else if (prefabPosX + _prefabSizeScript.lowestX + _prefabSizeScript.xOffset < 0)
                {
                    prefabPosX -= (prefabPosX + _prefabSizeScript.lowestX);
                }

                if (prefabPosY + _prefabSizeScript.highestY + _prefabSizeScript.yOffset > height.Value)
                {
                    prefabPosY -= (prefabPosY + _prefabSizeScript.highestY) - (height.Value);
                }
                else if (prefabPosY + _prefabSizeScript.lowestY + _prefabSizeScript.yOffset < height.Key)
                {
                    prefabPosY -= (prefabPosY + _prefabSizeScript.lowestY) - (height.Key);
                }

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
        KeyValuePair<float, float> height = GameManager.Instance.LayerManager.GetLevelHeight();

        float roll = Random.Range(0.0f, 100.0f);
        float chance = scatter.scatterChance;
        for (int i = 0; i < iteration; i++)
        {
            chance -= chance * (scatter.scatterChanceDegredation / 100.0f);
        }

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