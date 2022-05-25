using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public void setBossMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetBossHealth(float health)
    {
        slider.value = health;
    }
}
