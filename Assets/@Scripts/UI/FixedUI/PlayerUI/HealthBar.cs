using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;

    private void OnEnable()
    {
        var player = GameManager.Instance.player;
        if (player != null)
        {
            player.OnHealthChanged += UpdateHealthUI;
            player.OnManaChanged += UpdateManaUI;
        }
    }

    private void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    private void UpdateManaUI(float currentMana, float maxMana)
    {
        manaSlider.value = currentMana / maxMana;
    }
}
