using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageImageUI : MonoBehaviour
{
    public RectTransform imageRectTransform;
    public TextMeshProUGUI textComponent;
    public float duration = 1.0f;

    private void Start()
    {
        imageRectTransform.sizeDelta = new Vector2(0, imageRectTransform.sizeDelta.y);
        textComponent.alpha = 0;
    }

    public void StartStageUI(string text)
    {
        SetText(text);
        StartCoroutine(StageImage());
    }

    private void SetText(string text)
    {
        textComponent.text = text;
    }

    IEnumerator StageImage()
    {
        yield return imageRectTransform.DOSizeDelta(new Vector2(600, imageRectTransform.sizeDelta.y), duration).SetEase(Ease.Linear).WaitForCompletion();
        textComponent.DOFade(1f, duration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(3f);
        textComponent.DOFade(0f, duration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);
        yield return imageRectTransform.DOSizeDelta(new Vector2(0, imageRectTransform.sizeDelta.y), duration).SetEase(Ease.Linear).WaitForCompletion();
    }
}
