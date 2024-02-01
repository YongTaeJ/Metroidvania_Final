using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScriptForAnything : MonoBehaviour
{
    int ID = 0;

    private void Awake()
    {
        GameObject targetObj = Resources.Load<GameObject>("Enemies/Bosses/Boss_VillageHead");
        Instantiate(targetObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void AddddddddddMoney()
    {
        ItemManager.Instance.AddItem(ItemType.Gold, 0, 10);
        UIManager.Instance.PopupUI(PopupType.Status);
    }

    public void chattest()
    {
        ChatManager.Instance.StartChatting(ID++);
    }
}
