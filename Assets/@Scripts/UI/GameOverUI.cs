using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private TextMeshProUGUI tipText;
    private Button continueBtn;

    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        tipText = transform.Find("TipTxt").GetComponent<TextMeshProUGUI>();
        continueBtn = transform.Find("ContinueBtn").GetComponent <Button>();
        canvasGroup.alpha = 0;

        continueBtn.onClick.AddListener(RestartGame);

        tipText.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(false);

        ShowGameOverUI();
    }

    public void ShowGameOverUI()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        UIManager.Instance.SetFixedUI(false);
        float duration = 1.0f;
        float targetAlpha = 1.0f;

        while (canvasGroup.alpha < targetAlpha)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime / duration);
            yield return null;
        }

        tipText.gameObject.SetActive(true);
        continueBtn.gameObject.SetActive(true);

        StartCoroutine(FadeInResultText());
    }

    private IEnumerator FadeInResultText()
    {
        float duration = 1.0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            tipText.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void RestartGame()
    {
        UIManager.Instance.SetFixedUI(true);
        Debug.Log("마을로 돌아가기");
        GameManager.Instance.player.OnContinue();
    }
}
