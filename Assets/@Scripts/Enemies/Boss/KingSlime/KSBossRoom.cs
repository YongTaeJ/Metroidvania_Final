using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSBossRoom : BossRoom
{
    #region variables
    [SerializeField] private int _chatID_encounter_0;
    [SerializeField] private int _chatID_encounter_1;
    [SerializeField] private int _chatID_defeat_0;
    [SerializeField] private int _chatID_defeat_1;
    private KSEventSet _KSEventSet;
    private GameObject _KingSlimePrefab;
    private KSSpecialPattern _pattern;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _KingSlimePrefab = Resources.Load<GameObject>("Enemies/Bosses/KingSlime/KingSlime");
        _pattern = GameObject.Find("KSSpecialPattern").GetComponent<KSSpecialPattern>();
    }

    protected override IEnumerator EnterBossRoom()
    {
        DoorControl(true);
        List<(string, string)> chatDatas;
        _playerInput.enabled = false;

        _playerController.Move(Vector2.right);
        yield return new WaitForSeconds(0.3f);
        _playerController.Move(Vector2.zero);

        chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter_0);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        // 떨어지는 슬라임 보스 프리셋
        _KSEventSet = Instantiate(Resources.Load<GameObject>("Enemies/Bosses/KingSlime/KSEventSet"), _bossLocation.position, Quaternion.identity).GetComponent<KSEventSet>();

        yield return new WaitForSeconds(1.5f);
        _KSEventSet.Talk();

        chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter_1);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        // 보스 생성 후 게임 시작
        PlayBossBGM();
        _playerInput.enabled = true;
        _currentBoss = Instantiate(_KingSlimePrefab, _KSEventSet.transform.position, Quaternion.identity);
        _KSEventSet.gameObject.SetActive(false);
    }

    protected override IEnumerator EndBattle()
    {
        List<(string, string)> chatDatas;
        
         _playerInput.enabled = false;

        _KSEventSet.transform.position = _deadLocation;
        _KSEventSet.gameObject.SetActive(true);

        chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_0);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        float time = _KSEventSet.Up();
        yield return new WaitForSeconds(time);

        DropManager.Instance.DropItem(_dropTableIndex, _deadLocation);
        chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_1);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        Destroy(_KSEventSet.gameObject);
        _pattern.DestroyEnemies();

        PlayStageBGM();
        DoorControl(false);
        _playerInput.enabled = true;
    }

    protected override void PlayStageBGM()
    {
        BGM.Instance.Stop();
        BGM.Instance.Play("Stage1", true);
    }
}
