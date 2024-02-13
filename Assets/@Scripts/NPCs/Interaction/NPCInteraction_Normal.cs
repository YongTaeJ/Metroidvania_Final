using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class NPCInteraction_Normal : NPCInteraction
{
    [SerializeField] private int _chatID;

    public override IEnumerator Interact(PlayerInput input)
    {
        StartInteract(input);

        var chatDatas = ChatManager.Instance.GetChatData(_chatID);

        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        EndInteract(input);
    }
}
