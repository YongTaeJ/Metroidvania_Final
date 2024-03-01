using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteYNPanel : YNPanel
{
    private void Awake()
    {
        Initialize();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }
    
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    protected override void OnClickNo()
    {
        CloseUI();
    }

    protected override void OnClickYes()
    {
        GameManager.Instance.DeleteSaveFile();
        SceneManager.LoadScene("StartScene");
        CloseUI();
    }

    private void OnDisable()
    {
        CloseUI();
    }
}
