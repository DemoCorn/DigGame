using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dirt_Loader : MonoBehaviour
{
    [SerializeField] private List<LevelRange> LevelRanges = new List<LevelRange>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Non_Player_Health health;

    // Start is called before the first frame update
    void Start()
    {
        foreach(DirtRange dirtRange in LevelRanges[SceneManager.GetActiveScene().buildIndex].dirtLayers)
        {
            if (gameObject.transform.position.y <= dirtRange.highest && gameObject.transform.position.y >= dirtRange.lowest)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
                spriteRenderer.sprite = dirtRange.sprite;
                health.SetHealth(dirtRange.health);
                break;
            }
        }
    }

    [System.Serializable]
    public class LevelRange
    {
        public LevelRange()
        {
        }

        public LevelRange(int lvl, List<DirtRange> dirtRange)
        {
            level = lvl;
            dirtLayers = dirtRange;
        }
        public int level;
        public List<DirtRange> dirtLayers = new List<DirtRange>();
    }

    [System.Serializable]
    public class DirtRange
    {
        public DirtRange()
        {
        }

        public DirtRange(Sprite objectSprite, int high, int low)
        {
            sprite = objectSprite;
            highest = high;
            lowest = low;
        }
        public float health;
        public Sprite sprite;
        public float highest;
        public float lowest;
    }
}