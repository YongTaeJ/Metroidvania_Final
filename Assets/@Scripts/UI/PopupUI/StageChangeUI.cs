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
        StartCoroutine(Fade(0, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0, true)); // FadeOut 호출 시 비활성화를 위한 파라미터 true 추가
    }

    // 코루틴에 비활성화 여부를 결정하는 추가 파라미터 include
    IEnumerator Fade(float start, float end, bool deactivateOnComplete = false)
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

        // FadeOut이 완료된 후에만 오브젝트를 비활성화
        if (deactivateOnComplete && end == 0)
        {
            UIManager.Instance.ClosePopupUI(PopupType.StageChange);
        }
    }
}
