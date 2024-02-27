using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageChangeUI : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1.0f;

    public void FadeIn(System.Action onComplete = null)
    {
        StartCoroutine(Fade(1.0f, 0.0f, onComplete));
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(Fade(0.0f, 1.0f, onComplete));
    }

    private IEnumerator Fade(float start, float end, System.Action onComplete = null)
    {
        float counter = 0f;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, counter / fadeDuration);

            Color currentColor = fadePanel.color;
            currentColor.a = alpha;
            fadePanel.color = currentColor;

            yield return null;
        }

        onComplete?.Invoke();
    }
}
