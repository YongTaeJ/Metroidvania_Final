using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using DG.Tweening.Core.Easing;

public class NPCInteraction_Normal : NPCInteraction
{
    [SerializeField] private int _chatID_gameStart;
    [SerializeField] private int _chatID_remainder;
    private bool _isFirst;

    protected override void Awake()
    {
        base.Awake();
        _isFirst = true;
    }

    public override IEnumerator Interact(PlayerInput input)
    {
        StartInteract(input);

        if(_isFirst)
        {
            // 게임 재시작하면 다시 말합니다.
            _isFirst = false;
            var chatDatas = ChatManager.Instance.GetChatData(_chatID_gameStart);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }
        else
        {
            var chatDatas = ChatManager.Instance.GetChatData(_chatID_remainder);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
        }

        EndInteract(input);
    }
}
