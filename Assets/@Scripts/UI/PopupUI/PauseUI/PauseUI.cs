using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    #region Field

    [Header("Buttons")]
    [SerializeField] private Button contiuneBtn;
    [SerializeField] private Button optionBtn;
    [SerializeField] private Button backToMainBtn;
    [SerializeField] private Button exitBtn;

    #endregion

    #region Unity Flow

    private void Start()
    {
        contiuneBtn.onClick.AddListener(OnContinueGame);
        optionBtn.onClick.AddListener(OnOption);
        backToMainBtn.onClick.AddListener(OnBackToMain);
        exitBtn.onClick.AddListener(OnExit);
    }

    #endregion

    #region OnClick

    private void OnContinueGame()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void OnOption()
    {
        // 옵션 팝업 띄우기
        //UIManager.Instance.PopupUI(PopupType.Option);
    }

    private void OnBackToMain()
    {
        //SceneManager.LoadScene("StartScene");
    }

    private void OnExit()
    {
        Application.Quit();
    }

    #endregion
}
