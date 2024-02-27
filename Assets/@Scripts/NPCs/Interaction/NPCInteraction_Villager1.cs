using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using DG.Tweening.Core.Easing;
using System.Collections.Generic;

public class NPCInteraction_Normal : NPCInteraction
{
    [SerializeField] private int _chatID_gameStart;
    [SerializeField] private int _chatID_remainder;
    [SerializeField] private int _chatID_buildComplete;
    private bool _isFirst;

    protected override void Awake()
    {
        base.Awake();
        _isFirst = true;
    }

    public override IEnumerator Interact(PlayerInput input)
    {
        StartInteract(input);
        List<(string, string)> chatDatas;

        if(ItemManager.Instance.HasItem(ItemType.Building, 4))
        {
            chatDatas = ChatManager.Instance.GetChatData(_chatID_buildComplete);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }
        else if(_isFirst)
        {
            _isFirst = false;
            chatDatas = ChatManager.Instance.GetChatData(_chatID_gameStart);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }

        chatDatas = ChatManager.Instance.GetChatData(_chatID_remainder);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        EndInteract(input);
    }
}
