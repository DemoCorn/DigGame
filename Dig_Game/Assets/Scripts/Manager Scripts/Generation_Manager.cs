using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation_Manager : MonoBehaviour
{
    public int levelWidth = 60;

    // Attach the dirt tile objects in Unity to these Transforms
    public GameObject dirtObj;
    public List<OreRange> oreRanges;

    public GameObject[] roomPrefabs; 

    private static int randomRoom;
    private static bool randomized = false;

    public static float prefabPosX;
    public static float prefabPosY;

    public GetPrefabSize _prefabSizeScript;

    // Start is called before the first frame update
    public void Start()
    {
        // xPos will determine the amount of spaces horizontally we want to span
        // The second value in each Vector2 is the spacing vertically between tiles
        // Adjust this value according to sprite being used

        // Call randomize once if it hasn't already been done
        if (!randomized)
        {
            Randomize();
        }
        Debug.Log("Generated prefab: " + randomRoom + " from list");

        // Instantiate dirt
        GameObject oreType;
        int mineralX;
        int mineralY;
        int oreAmount;

        // Instantiate roomPrefab and store script min and max values
        GameObject room = Instantiate(roomPrefabs[randomRoom], new Vector3(prefabPosX, prefabPosY, 0), Quaternion.identity);
        _prefabSizeScript = FindObjectOfType<GetPrefabSize>();

        if (_prefabSizeScript.maxX >= levelWidth)
        {
            room.transform.position = new Vector3(room.transform.position.x - (_prefabSizeScript.maxX - (levelWidth - 1)), room.transform.position.y, room.transform.position.z);
            _prefabSizeScript.minX -= _prefabSizeScript.maxX - (levelWidth - 1);
            _prefabSizeScript.maxX = levelWidth - 1;
        }
        else if(_prefabSizeScript.minX < 0)
        {
            room.transform.position = new Vector3(room.transform.position.x - (_prefabSizeScript.minX), room.transform.position.y, room.transform.position.z);
            _prefabSizeScript.maxX -= _prefabSizeScript.minX;
            _prefabSizeScript.minX = 0;
        }

        LevelRange levels = GameManager.Instance.LayerManager.GetLevelRange();
        for(int i = 0; i < levels.layerRange.Count; i++)
        {
            // Instantiate Dirt Blocks
            for (float xPos = 0f; xPos < levelWidth; xPos++)
            {
                for (float yPos = levels.layerRange[i].lowest; yPos <= levels.layerRange[i].highest; yPos++)
                {
                    // Place around the prefab
                    if ((((xPos < _prefabSizeScript.minX && xPos < _prefabSizeScript.maxX))
                        || (xPos > _prefabSizeScript.minX && xPos > _prefabSizeScript.maxX))
                        || ((yPos < _prefabSizeScript.minY && yPos < _prefabSizeScript.maxY)
                        || (yPos > _prefabSizeScript.minY && yPos > _prefabSizeScript.maxY)))
                    {
                        Instantiate(dirtObj, new Vector2(xPos, yPos), dirtObj.transform.rotation);
                    }
                }
            }

            oreAmount = Random.Range(oreRanges[i].lowestOreAmount, oreRanges[i].highestOreAmount + 1);

            // Random Minerals
            for (int j = 0; j < oreAmount; j++)
            {
                mineralX = Random.Range(0, levelWidth);
                mineralY = Random.Range((int) levels.layerRange[i].lowest, (int) levels.layerRange[i].highest + 1);

                oreType = oreRanges[i].oreAtLayer[Random.Range(0, oreRanges[i].oreAtLayer.Count)];

                // Destroy dirt blocks based on prefab size
                if ((((mineralX < _prefabSizeScript.minX && mineralX < _prefabSizeScript.maxX))
                    || (mineralX > _prefabSizeScript.minX && mineralX > _prefabSizeScript.maxX))
                    || ((mineralY < _prefabSizeScript.minY && mineralY < _prefabSizeScript.maxY)
                    || (mineralY > _prefabSizeScript.minY && mineralY > _prefabSizeScript.maxY)))
                {
                    Instantiate(oreType, new Vector2(mineralX, mineralY), oreType.transform.rotation);
                }
                else
                {
                    j -= 1;
                    continue;
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
        prefabPosX = Random.Range(0, levelWidth);
        prefabPosY = Random.Range(-3, -10);
        randomized = true;
    }

    [System.Serializable]
    public class OreRange
    {
        public OreRange()
        {
        }

        public OreRange(List<GameObject> oreLayer, int nLow, int nHigh)
        {
            oreAtLayer = oreLayer;
            lowestOreAmount = nLow;
            highestOreAmount = nHigh;
        }
        public List<GameObject> oreAtLayer = new List<GameObject>();
        public int lowestOreAmount;
        public int highestOreAmount;
    }
}