using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSBossRoom : BossRoom
{
    #region variables
    [SerializeField] private int _chatID_encounter_0;
    [SerializeField] private int _chatID_encounter_1;
    [SerializeField] private int _chatID_defeat_0;
    #endregion

    protected override IEnumerator EndBattle()
    {
        List<(string, string)> chatDatas;

        chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter_0);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter_1);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
    }

    protected override IEnumerator EnterBossRoom()
    {
        List<(string, string)> chatDatas;


        chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_0);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
    }
}
