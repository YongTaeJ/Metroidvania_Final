using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;

public class NPCInteraction_Quest : NPCInteraction
{
    [SerializeField] private int _questID;
    [SerializeField] private int _chatID_notAccept;
    [SerializeField] private int _chatID_acceptQuest;
    [SerializeField] private int _chatID_refuseQuest;
    [SerializeField] private int _chatID_alreadyAccept;
    [SerializeField] private int _chatID_alreadyComplete;
    [SerializeField] private int _chatID_completeQuest;

    public override IEnumerator Interact(PlayerInput input)
    {
        List<(string, string)> chatDatas;
        StartInteract(input);

        bool isNotAccept = false;

        switch(ItemManager.Instance.GetItemStock(ItemType.Quest, _questID))
        {
            case 0:
            {
                chatDatas = ChatManager.Instance.GetChatData(_chatID_notAccept);
                yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
                isNotAccept = true;
            }
                break;
            case 1:
            {
                chatDatas = ChatManager.Instance.GetChatData(_chatID_alreadyAccept);
                yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
                if(CanQuestClear())
                {
                    chatDatas = ChatManager.Instance.GetChatData(_chatID_completeQuest);
                    yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
                    ItemManager.Instance.AddItem(ItemType.Quest, _questID);
                    AddReward();
                }
            }
                break;
            case 2:
            {
                chatDatas = ChatManager.Instance.GetChatData(_chatID_alreadyComplete);
                yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
            }
                break;
        }

        if(isNotAccept)
        {
            yield return StartCoroutine(WaitForChoose());
        }

        EndInteract(input);
    }

    private IEnumerator WaitForChoose()
    {
        int buttonValue = 0;

        _chatBoxUI.MakeButton("네", () => buttonValue = 1);
        _chatBoxUI.MakeButton("싫음", () => buttonValue = 2);

        while(buttonValue == 0)
        {
            yield return null;
        }

        _chatBoxUI.CloseButtons();

        if(buttonValue == 1)
        {
            ItemManager.Instance.AddItem(ItemType.Quest, _questID);
            var chatDatas = ChatManager.Instance.GetChatData(_chatID_acceptQuest);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }
        else
        {
            var chatDatas = ChatManager.Instance.GetChatData(_chatID_refuseQuest);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }
    }

    private bool CanQuestClear()
    {
        var SO = SOManager.Instance.GetQuestSO(_questID);

        foreach(var item in SO.RequriedItems)
        {
            if(ItemManager.Instance.GetItemStock(item.ItemType, item.ID) < item.Stock)
            {
                return false;
            }
        }

        return true;
    }
    
    private void AddReward()
    {
        var SO = SOManager.Instance.GetQuestSO(_questID);

        foreach(var item in SO.Rewards)
        {
            ItemManager.Instance.AddItem(item.ItemType, item.ID, item.Stock);
        }
    }
}
