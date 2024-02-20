using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSBossRoom : BossRoom
{
    #region variables
    [SerializeField] private int _chatID_encounter_0;
    [SerializeField] private int _chatID_encounter_1;
    [SerializeField] private int _chatID_defeat_0;
    private KSEventSet _KSEventSet;
    private GameObject _KingSlimePrefab;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _KingSlimePrefab = Resources.Load<GameObject>("Enemies/Bosses/KingSlime/KingSlime");
    }

    protected override IEnumerator EnterBossRoom()
    {
        List<(string, string)> chatDatas;

        chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter_0);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        // 떨어지는 슬라임 보스 프리셋
        _KSEventSet = Instantiate(Resources.Load<GameObject>("Enemies/Bosses/KingSlime/KSEventSet"), _bossLocation.position, Quaternion.identity).GetComponent<KSEventSet>();

        yield return new WaitForSeconds(1.5f);
        _KSEventSet.Talk();

        chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter_1);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        // 보스 생성 후 게임 시작
        Instantiate(_KingSlimePrefab, _KSEventSet.transform.position, Quaternion.identity);
        _KSEventSet.gameObject.SetActive(false);
    }

    protected override IEnumerator EndBattle()
    {
        List<(string, string)> chatDatas;

        _KSEventSet.transform.position = _deadLocation;
        _KSEventSet.gameObject.SetActive(true);

        chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_0);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        // TODO => 점프해서 올라가는 느낌으로 사라지기

        DropManager.Instance.DropItem(_dropTableIndex, _deadLocation);
        DoorControl(false);
        _playerInput.enabled = true;
    }


}
