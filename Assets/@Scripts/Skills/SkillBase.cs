using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{
    public GameObject skillUI {  get; private set; }
    public Image cooldownImage { get; private set; }
    public TextMeshProUGUI cooldownText { get; private set; }
    public float Cooldown { get; protected set; }

    private float _cooldown;

    protected virtual void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
            float fillAmount = _cooldown / 5f;
            cooldownImage.fillAmount = fillAmount;
            cooldownText.text = Mathf.CeilToInt(_cooldown).ToString();
        }
        else
        {
            cooldownImage.fillAmount = 0;
            cooldownText.text = "0";
            cooldownText.gameObject.SetActive(false);
        }
    }

    public virtual void Initialize()
    {
        skillUI = GameObject.Find(GetType().Name);
        cooldownImage = skillUI.transform.Find("CooldownImage").GetComponent<Image>();
        cooldownText = skillUI.transform.Find("CooldownText").GetComponent<TextMeshProUGUI>();
        cooldownImage.fillAmount = 0;
        cooldownText.text = "0";
        cooldownText.gameObject.SetActive(false);
    }

    public virtual bool Activate()
    {
        if (_cooldown > 0) return false;
        _cooldown = Cooldown;

        cooldownText.gameObject.SetActive(true);
        return true;
    }
}
