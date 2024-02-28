using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageImageUI : MonoBehaviour
{
    public RectTransform imageRectTransform;
    public TextMeshProUGUI textComponent;
    public float duration = 0.2f;

    private void Start()
    {
        imageRectTransform.sizeDelta = new Vector2(0, imageRectTransform.sizeDelta.y);
        textComponent.alpha = 0;
    }

    public void InitializeUI()
    {
        imageRectTransform.sizeDelta = new Vector2(0, imageRectTransform.sizeDelta.y);
        textComponent.alpha = 0;
        textComponent.text = "";
    }

    public void StartStageUI(string text)
    {
        InitializeUI();
        SetText(text);
        StartCoroutine(StageImage());
    }

    private void SetText(string text)
    {
        textComponent.text = text;
    }

    IEnumerator StageImage()
    {
        yield return imageRectTransform.DOSizeDelta(new Vector2(600, imageRectTransform.sizeDelta.y), duration).SetEase(Ease.OutBounce).WaitForCompletion();
        yield return textComponent.DOFade(1f, duration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(3f);
        textComponent.DOFade(0f, duration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.5f);
        yield return imageRectTransform.DOSizeDelta(new Vector2(0, imageRectTransform.sizeDelta.y), duration).SetEase(Ease.Linear).WaitForCompletion();
    }
}
