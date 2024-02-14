using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI_SwordAuror : MonoBehaviour
{
    private Image cooldownImage;
    private TextMeshProUGUI cooldownText;

    private void Awake()
    {
        cooldownImage = transform.Find("CooldownImage").GetComponent<Image>();
        cooldownText = transform.Find("CooldownText").GetComponent<TextMeshProUGUI>();
        cooldownImage.fillAmount = 0;
        cooldownText.text = "0";
        cooldownText.gameObject.SetActive(false);
        
    }
}
