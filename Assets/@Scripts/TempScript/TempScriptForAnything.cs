using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScriptForAnything : MonoBehaviour
{
    int ID = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void chattest()
    {
    }

    public void OpenShop()
    {
        UIManager.Instance.OpenShopUI(0);
    }
}
