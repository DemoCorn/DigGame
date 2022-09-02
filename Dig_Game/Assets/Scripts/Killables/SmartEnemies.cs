using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smart_Enemies : MonoBehaviour
{
    /*
    [SerializeField] private int jaggedRange = 2;

    [SerializeField] private bool overrideLayerHealth = false;
    [SerializeField] private List<Range> levelRanges = new List<Range>();
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioSource audioSource;
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
                TileSet(levels, i);
                break;
            }
        }
    }

    private bool TileSet(LevelRange levels, int index)
    {
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
        spriteRenderer.sprite = levelRanges[levels.nLevelNumber].dirtLayers[index].sprite[Random.Range(0, levelRanges[levels.nLevelNumber].dirtLayers[index].sprite.Count)];
        audioSource.clip = levelRanges[levels.nLevelNumber].dirtLayers[index].audio;
        ParticleSystem.MainModule main = particle.main;
        main.startColor = levelRanges[levels.nLevelNumber].dirtLayers[index].effectColour;

        if (!overrideLayerHealth)
        {
            health.SetHealth(levelRanges[levels.nLevelNumber].dirtLayers[index].health);
            health.SetMaxHealth(levelRanges[levels.nLevelNumber].dirtLayers[index].maxHealth);
        }
        return true;
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

        public DirtRange(float hp, float dmg, Color colour)
        {
            health = hp;
            damage = dmg;
            enemyColour = colour;
        }
        public float health;
        public float damage;
        public Color enemyColour;
    }
    */
}
