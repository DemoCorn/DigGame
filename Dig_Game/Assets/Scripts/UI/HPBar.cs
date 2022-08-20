using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image healthBar;
    public Slider healthSlider;
    public Color FullColour;
    public Color EmptyColour;

    private void Start()
    {
        GameManager.Instance.UIManager.SetBar(this);
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthBar.color = (FullColour - EmptyColour) * currentHealth / maxHealth + EmptyColour;
        healthSlider.value = currentHealth / maxHealth;
    }
}
