using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction_VillageHead : NPCInteraction
{
    [SerializeField] private int _chatID_normal;
    [SerializeField] private int _chatID_allClear;

    protected override void Awake()
    {
        base.Awake();
    }

    public override IEnumerator Interact(PlayerInput input)
    {
        StartInteract(input);

        if(IsAllClear())
        {
            var chatDatas = ChatManager.Instance.GetChatData(_chatID_allClear);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }
        else
        {
            var chatDatas = ChatManager.Instance.GetChatData(_chatID_normal);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }

        EndInteract(input);
    }

    public bool IsAllClear()
    {
        var curBuildinginfo = ItemManager.Instance.GetItemDict(ItemType.Building);

        foreach (var info in curBuildinginfo.Values)
        {
            if(info.Stock == 0)
            {
                return false;
            }
        }

        // 전부 통과하면 ...
        return true;
    }
}
