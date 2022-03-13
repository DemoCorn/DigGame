using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public List<Range> levelRanges = new List<Range>();

    // Start is called before the first frame update
    void Start()
    {
        LevelRange levels = GameManager.Instance.LayerManager.GetLevelRange();
        for (int i = 0; i < levelRanges[levels.nLevelNumber].enemyLayer.Count; i++)
        {
            if (gameObject.transform.position.y <= levels.layerRange[i].highest && gameObject.transform.position.y >= levels.layerRange[i].lowest)
            {
                Instantiate(levelRanges[levels.nLevelNumber].enemyLayer[i], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, gameObject.transform);
            }
        }

        Destroy(gameObject);
    }

    [System.Serializable]
    public class Range
    {
        public Range()
        {
        }

        public Range(List<GameObject> enemyRange)
        {
            enemyLayer = enemyRange;
        }
        public List<GameObject> enemyLayer = new List<GameObject>();
    }
}
