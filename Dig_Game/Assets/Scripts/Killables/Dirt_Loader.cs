using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dirt_Loader : MonoBehaviour
{
    [SerializeField] private int jaggedRange = 2;

    [SerializeField] private bool overrideLayerHealth = false;
    [SerializeField] private List<Range> levelRanges = new List<Range>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Non_Player_Health health;

    // Start is called before the first frame update
    void Start()
    {
        LevelRange levels = GameManager.Instance.LayerManager.GetLevelRange();

        int offset = Random.Range(-jaggedRange, jaggedRange+1);
        bool rendered = false;

        for (int i = 0; i < levelRanges[levels.nLevelNumber].dirtLayers.Count; i++)
        {
            if (gameObject.transform.position.y + offset <= levels.layerRange[i].highest && gameObject.transform.position.y + offset >= levels.layerRange[i].lowest)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
                spriteRenderer.sprite = levelRanges[levels.nLevelNumber].dirtLayers[i].sprite;
                if (!overrideLayerHealth)
                {
                    health.SetHealth(levelRanges[levels.nLevelNumber].dirtLayers[i].health);
                }
                rendered = true;
                break;
            }
        }

        // Fixes issue where jagged makes untextured blocks
        if (!rendered)
        {
            if (gameObject.transform.position.y <= levels.layerRange[0].highest && gameObject.transform.position.y >= levels.layerRange[0].lowest)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
                spriteRenderer.sprite = levelRanges[levels.nLevelNumber].dirtLayers[0].sprite;
                if (!overrideLayerHealth)
                {
                    health.SetHealth(levelRanges[levels.nLevelNumber].dirtLayers[0].health);
                }
                rendered = true;
            }
            else if (gameObject.transform.position.y <= levels.layerRange[levels.layerRange.Count - 1].highest && gameObject.transform.position.y >= levels.layerRange[levels.layerRange.Count - 1].lowest)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
                spriteRenderer.sprite = levelRanges[levels.nLevelNumber].dirtLayers[levels.layerRange.Count - 1].sprite;
                if (!overrideLayerHealth)
                {
                    health.SetHealth(levelRanges[levels.nLevelNumber].dirtLayers[levels.layerRange.Count - 1].health);
                }
                rendered = true;
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