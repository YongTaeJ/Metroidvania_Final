using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScriptForAnything : MonoBehaviour
{
    int ID = 0;
    public void AddddddddddMoney()
    {
        ItemManager.Instance.AddItem(ItemType.Gold, 0, 10);
        UIManager.Instance.OpenPopupUI(PopupType.Status);
    }

    public void chattest()
    {
    }

    public void OpenShop()
    {
        UIManager.Instance.OpenShopUI(0);
    }

    public void OpenStatus()
    {
        UIManager.Instance.OpenPopupUI(PopupType.Status);
    }
}
