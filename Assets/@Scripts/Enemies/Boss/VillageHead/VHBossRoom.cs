using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class VHBossRoom : BossRoom
{

    #region variables
    [SerializeField] private int _chatID_encounter;
    [SerializeField] private int _chatID_defeat_0;
    [SerializeField] private int _chatID_defeat_1;
    private VHEntrySet _VHEntrySet;
    private GameObject _VillageHeadPrefab;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _VillageHeadPrefab = Resources.Load<GameObject>("Enemies/Bosses/VillageHead/Boss_VillageHead");
    }

    protected override IEnumerator EnterBossRoom()
    {
        List<(string, string)> chatDatas;

        // 컨트롤러를 끊고, 캐릭터가 적당한 위치까지 오도록 만듭니다.
        DoorControl(true);
        _playerInput.enabled = false;

        _playerController.Move(Vector2.right);
        yield return new WaitForSeconds(0.3f);
        _playerController.Move(Vector2.zero);

        // 이벤트 전용 마을 촌장 게임 오브젝트를 생성하고, 조종하기 위해 참조를 할당합니다.
        _VHEntrySet = Instantiate
        (Resources.Load<GameObject>("Enemies/Bosses/VillageHead/VillageHead_EntrySet"), _bossLocation.position, Quaternion.identity)
        .GetComponent<VHEntrySet>();
        _VHEntrySet.Initialize(this);

        // 참조를 통해 모션을 재생하고, 일련의 행동이 끝나면 전투를 시작합니다.
        _VHEntrySet.Jump();
        yield return new WaitForSeconds(2.5f);

        chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        PlayBossBGM();
        _VHEntrySet.Taunt();
    }

    protected override IEnumerator EndBattle()
    {
        List<(string, string)> chatDatas;

        _playerInput.enabled = false;

        // 이벤트 전용 마을 촌장 게임 오브젝트를 생성하고, 조종하기 위해 참조를 할당합니다.
        VHEndSet endSet = Instantiate(Resources.Load("Enemies/Bosses/VillageHead/VHEndSet"), _deadLocation, Quaternion.identity).GetComponent<VHEndSet>();

        chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_0);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        endSet.TriggerEffect();

        chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_1);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        // 전투가 끝났으므로 문을 열고, 아이템을 드랍합니다.
        PlayStageBGM();
        ItemManager.Instance.AddItem(ItemType.NPC, 4);
        DropManager.Instance.DropItem(_dropTableIndex, _deadLocation);
        DoorControl(false);
        _playerInput.enabled = true;
    }

    public void BattleStart()
    {
        _currentBoss = Instantiate(_VillageHeadPrefab, _VHEntrySet.transform.position, Quaternion.identity);
        _playerInput.enabled = true;
    }

    protected override void PlayStageBGM()
    {
        BGM.Instance.Stop();
        BGM.Instance.Play("Stage01", true);
    }
}
