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
    [SerializeField] private Button saveDateDelBtn;
    [SerializeField] private Button exitBtn;

    #endregion

    #region Unity Flow

    private void Start()
    {
        contiuneBtn.onClick.AddListener(OnContinueGame);
        optionBtn.onClick.AddListener(OnOption);
        saveDateDelBtn.onClick.AddListener(OnSaveDateDel);
        exitBtn.onClick.AddListener(OnExit);
    }

    #endregion

    #region OnClick

    private void OnContinueGame()
    {
        UIManager.Instance.ClosePopupUI(PopupType.Pause);
        Time.timeScale = 1.0f;
    }

    private void OnOption()
    {
        UIManager.Instance.ClosePopupUI(PopupType.Pause);
        UIManager.Instance.OpenPopupUI(PopupType.Option);
    }

    private void OnSaveDateDel()
    {
        GameManager.Instance.DeleteSaveFile();
    }

    private void OnExit()
    {
        Application.Quit();
    }

    #endregion
}
