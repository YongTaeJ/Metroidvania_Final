using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;

public class NPCInteraction_Shop : NPCInteraction
{
    [SerializeField] private int _shopID;
    [SerializeField] private int _chatID_first;
    [SerializeField] private int _chatID_start;
    [SerializeField] private int _chatID_end;
    private bool _isFirst = false;

    public override IEnumerator Interact(PlayerInput input)
    {
        StartInteract(input);

        List<(string, string)> chatDatas;

        if(!_isFirst)
        {
            _isFirst = true;
            chatDatas = ChatManager.Instance.GetChatData(_chatID_first);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }
        else
        {
            chatDatas = ChatManager.Instance.GetChatData(_chatID_start);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }

        yield return StartCoroutine(WaitForChoose());

        chatDatas = ChatManager.Instance.GetChatData(_chatID_end);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        EndInteract(input);
    }

    private IEnumerator WaitForChoose()
    {
        int buttonValue = 0;

        _chatBoxUI.MakeButton("구매하기", () => buttonValue = 1);
        _chatBoxUI.MakeButton("그만두기", () => buttonValue = 2);

        while(buttonValue == 0)
        {
            yield return null;
        }

        _chatBoxUI.CloseButtons();

        if(buttonValue == 1)
        {
            UIManager.Instance.OpenShopUI(_shopID);
            var obj = UIManager.Instance.GetUI(PopupType.Shop);
            while(obj.activeInHierarchy)
            {
                yield return null;
            }
        }
    }
}
