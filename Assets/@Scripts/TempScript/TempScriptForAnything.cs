using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScriptForAnything : MonoBehaviour
{
    public void AddddddddddMoney()
    {
        ItemManager.Instance.AddItem(ItemType.Gold, 0, 10);
        UIManager.Instance.OnPopupEvent(PopupType.Status);
    }
}
