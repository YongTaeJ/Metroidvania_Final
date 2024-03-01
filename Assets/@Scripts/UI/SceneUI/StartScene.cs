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

    #endregion

    #region Unity Flow

    private void Start()
    {
        startBtn.onClick.AddListener(OnStartGame);
        BGM.Instance.Play("StartScene", true);
    }

    #endregion

    #region OnClick

    private void OnStartGame()
    {
        Debug.Log("버튼 누름");
        BGM.Instance.Stop();
        //BGM.Instance.Play(BGM.Instance.Home, true); 메인씬 스타트에서 시작
        SceneManager.LoadScene("LoadingScene");
    }

    #endregion
}
