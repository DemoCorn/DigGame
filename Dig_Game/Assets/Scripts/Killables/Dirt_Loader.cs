using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dirt_Loader : MonoBehaviour
{
    [SerializeField] private bool overrideLayerHealth = false;
    [SerializeField] private List<Range> levelRanges = new List<Range>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Non_Player_Health health;

    // Start is called before the first frame update
    void Start()
    {
        LevelRange levels = GameManager.Instance.LayerManager.GetLevelRange();
        for (int i = 0; i < levelRanges[levels.nLevelNumber].dirtLayers.Count; i++)
        {
            if (gameObject.transform.position.y <= levels.layerRange[i].highest && gameObject.transform.position.y >= levels.layerRange[i].lowest)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
                spriteRenderer.sprite = levelRanges[levels.nLevelNumber].dirtLayers[i].sprite;
                if (!overrideLayerHealth)
                {
                    health.SetHealth(levelRanges[levels.nLevelNumber].dirtLayers[i].health);
                }
                break;
            }
        }
    }

    [System.Serializable]
    public class Range
    {
        public Range()
        {
        }

        public Range(List<DirtRange> dirtRange)
        {
            dirtLayers = dirtRange;
        }
        public List<DirtRange> dirtLayers = new List<DirtRange>();
    }

    [System.Serializable]
    public class DirtRange
    {
        public DirtRange()
        {
        }

        public DirtRange(float hp, Sprite objectSprite)
        {
            health = hp;
            sprite = objectSprite;
        }
        public float health;
        public Sprite sprite;
    }
}