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
        StartCoroutine(InitializeWhenPlayerIsReady());
    }

    private IEnumerator InitializeWhenPlayerIsReady()
    {
        // GameManager.Instance.player가 null이 아닐 때까지 기다립니다.
        yield return new WaitUntil(() => GameManager.Instance.player != null);

        // 이제 player가 null이 아니므로 이벤트에 메소드를 구독합니다.
        GameManager.Instance.player.OnHealthChanged += UpdateHealthUI;
        GameManager.Instance.player.OnManaChanged += UpdateManaUI;

        Debug.Log("초기화 됨");
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
