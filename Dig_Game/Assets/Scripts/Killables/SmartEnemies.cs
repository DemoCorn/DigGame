using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smart_Enemies : MonoBehaviour
{

    [SerializeField] private bool overrideLayerHealth = false;
    [SerializeField] private List<Range> levelRanges = new List<Range>();
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Non_Player_Health health;
    [SerializeField] private Enemy_Damage damage;

    // Start is called before the first frame update
    void Start()
    {
        LevelRange levels = GameManager.Instance.LayerManager.GetLevelRange();

        for (int i = 0; i < levelRanges[levels.nLevelNumber].enemyLayers.Count; i++)
        {
            if (gameObject.transform.position.y <= levels.layerRange[i].highest && gameObject.transform.position.y >= levels.layerRange[i].lowest)
            {
                StatSet(levels, i);
                break;
            }
        }
    }

    private bool StatSet(LevelRange levels, int index)
    {
        spriteRenderer.color = levelRanges[levels.nLevelNumber].enemyLayers[index].enemyColour;
        damage.damage = levelRanges[levels.nLevelNumber].enemyLayers[index].damage;

        if (!overrideLayerHealth)
        {
            health.SetHealth(levelRanges[levels.nLevelNumber].enemyLayers[index].health);
            health.SetMaxHealth(levelRanges[levels.nLevelNumber].enemyLayers[index].health);
        }
        return true;
    }

    [System.Serializable]
    public class Range
    {
        public Range()
        {
        }

        public Range(List<EnemyRange> dirtRange)
        {
            enemyLayers = dirtRange;
        }
        public List<EnemyRange> enemyLayers = new List<EnemyRange>();
    }

    [System.Serializable]
    public class EnemyRange
    {
        public EnemyRange()
        {
        }

        public EnemyRange(float hp, float dmg, Color colour)
        {
            health = hp;
            damage = dmg;
            enemyColour = colour;
        }
        public float health;
        public float damage;
        public Color enemyColour;
    }
}
