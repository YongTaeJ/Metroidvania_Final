using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    #region Field

    [Header("Buttons")]
    [SerializeField] private Button startBtn;
    [SerializeField] private Button contiuneBtn;
    [SerializeField] private Button optionBtn;
    [SerializeField] private Button exitBtn;

    #endregion

    #region Unity Flow

    private void Start()
    {
        startBtn.onClick.AddListener(OnStartGame);
        contiuneBtn.onClick.AddListener(OnContinueGame);
        optionBtn.onClick.AddListener(OnOption);
        exitBtn.onClick.AddListener(OnExit);
    }

    #endregion

    #region OnClick

    private void OnStartGame()
    {
        //SceneManager.LoadScene("GameScene");
    }

    private void OnContinueGame()
    {
        // 저장된 데이터 불러오기
    }

    private void OnOption()
    {
        // 옵션 팝업 띄우기
    }

    private void OnExit()
    {
        // 게임종료
    }

    #endregion
}
