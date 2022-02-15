using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer_Manager : MonoBehaviour
{
    [SerializeField] List<LevelRange> LevelRanges = new List<LevelRange>();

    public void Start()
    {
        for(int i = 0; i < LevelRanges.Count; i++)
        {
            LevelRanges[i].nLevelNumber = i;
        }
    }

    public LevelRange GetLevelRange()
    {
        return LevelRanges[GameManager.Instance.GetLevelNum()];
    }

    public KeyValuePair<float, float> GetLevelHeight()
    {
        LevelRange levelRange = GetLevelRange();
        float fLowest = levelRange.layerRange[levelRange.layerRange.Count - 1].lowest;
        float fHighest = levelRange.layerRange[0].highest;
        return new KeyValuePair<float, float>(fLowest, fHighest);
    }
}

[System.Serializable]
public class LayerRange
{
    public LayerRange()
    {
    }

    public LayerRange(float key, float value)
    {
        highest = key;
        lowest = value;
    }

    public float highest;
    public float lowest;
}

[System.Serializable]
public class LevelRange
{
    public LevelRange()
    {
    }

    public LevelRange(List<LayerRange> layerRanges)
    {
        layerRange = layerRanges;
    }
    public List<LayerRange> layerRange = new List<LayerRange>();
    [HideInInspector] public int nLevelNumber;
}
