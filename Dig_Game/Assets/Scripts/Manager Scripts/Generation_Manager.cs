using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation_Manager : MonoBehaviour
{
    public int levelWidth = 60;

    // Attach the dirt tile objects in Unity to these Transforms
    public GameObject dirtObj;
    public List<LevelOre> oreLevelRanges;

    public List<LevelPrefab> levelPrefabs;

    private static int randomRoom;
    private static bool randomized = false;

    public static float prefabPosX;
    public static float prefabPosY;

    public GetPrefabSize _prefabSizeScript;

    private HashSet<KeyValuePair<int, int>> reservedSpaces = new HashSet<KeyValuePair<int, int>>();

    // Start is called before the first frame update
    public void Start()
    {
        // xPos will determine the amount of spaces horizontally we want to span
        // The second value in each Vector2 is the spacing vertically between tiles
        // Adjust this value according to sprite being used

        KeyValuePair<float, float> height = GameManager.Instance.LayerManager.GetLevelHeight();

        // Call randomize once if it hasn't already been done
        if (!randomized)
        {
            Randomize();
        }
        Debug.Log("Generated prefab: " + randomRoom + " from list");

        // Instantiate dirt
        int mineralX;
        int mineralY;
        int oreAmount;
        GameObject room;
        bool reserved = false;

        LevelRange levels = GameManager.Instance.LayerManager.GetLevelRange();
        List<PrefabRange> prefabRanges = levelPrefabs[levels.nLevelNumber].prefabAtLevel;
        List<OreRange> oreRanges = oreLevelRanges[levels.nLevelNumber].oreAtLevel;
        for (int i = 0; i < levels.layerRange.Count; i++)
        {
            randomRoom = Random.Range(0, prefabRanges[i].prefabAtLayer.Count);
            _prefabSizeScript = roomPrefabs[randomRoom].GetComponent<GetPrefabSize>();
            do
            {
                reserved = false;
                prefabPosX = Random.Range(0, levelWidth);
                prefabPosY = Random.Range((int)height.Key, (int)height.Value);

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
                    room = Instantiate(roomPrefabs[randomRoom], new Vector3(prefabPosX, prefabPosY, 0), Quaternion.identity);
                }
            } while (reserved);



            foreach (OreType ore in oreRanges[i].oreAtLayer)
            {
                // Random Minerals
                oreAmount = Random.Range(ore.lowestOreAmount, ore.highestOreAmount + 1);

                for (int j = 0; j < oreAmount; j++)
                {
                    mineralX = Random.Range(0, levelWidth);
                    mineralY = Random.Range((int) levels.layerRange[i].lowest, (int) levels.layerRange[i].highest + 1);

                    // Destroy dirt blocks based on prefab size
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

    public void Randomize()
    {
        //Randomize which room out of the array to select
        // Note - passing min and max INT values into the range overload the function to a (minINCLUSIVE, maxEXCLUSIVE) range.
        // Note - passing min and max FLOAT values into the range overload the function to a (minINCLUSIVE, maxINCLUSIVE) range.
        randomRoom = Random.Range(0, roomPrefabs.Length);

        // Randomizing chosen condition to create opening for both X & Y values of attached tile
        randomized = true;
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

    // Prefab Classes --------------------------------------------------------------------------------------------------
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

        public PrefabRange(List<PrefabType> prefabs)
        {
            prefabAtLayer = prefabs;
        }
        public List<PrefabType> prefabAtLayer = new List<PrefabType>();
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
        GameObject prefab;
    }
}