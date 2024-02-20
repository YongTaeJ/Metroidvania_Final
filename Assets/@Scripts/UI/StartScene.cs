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
    }

    #endregion

    #region OnClick

    private void OnStartGame()
    {
        //SceneManager.LoadScene("GameScene");
    }

    #endregion
}
