using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedDirt : MonoBehaviour
{
    [SerializeField] Sprite currentSprite;
    [SerializeField] List<Sprite> crackedDirts = new List<Sprite>();
    [SerializeField] List<float> crackPercentage = new List<float>();

    [SerializeField] SpriteRenderer rend;

    private void Start()
    {
        rend.color = new Color(0f, 0f, 0f, 0f);
    }

    // Update is called once per frame
    public void SetCracks(float health, float maxHealth)
    {
        float crackPercent = health / maxHealth;

        Debug.Log(crackPercent);

        for (int i = crackPercentage.Count -1; i >= 0; i--)
        {
            if (crackPercent <= crackPercentage[i])
            {
                Debug.Log(i);
                rend.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                rend.sprite = crackedDirts[i];
                break;
            }
        }
    }

    public void DestroyCracks()
    {
        Destroy(gameObject);
    }
}
