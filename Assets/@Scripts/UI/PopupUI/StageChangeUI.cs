using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageChangeUI : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1.0f;

    private void OnEnable()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1));
    }

    public void StartFadeOut()
    {
        StartCoroutine(Fade(1, 0, () => gameObject.SetActive(false)));
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
