using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private Vector3 _deadLocation;
    #endregion

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

        // 참조를 통해 모션을 재생하고, 일련의 행동이 끝나면 전투를 시작합니다.
        _VHEntrySet.Jump();
        yield return new WaitForSeconds(2.5f);

        chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

        _VHEntrySet.Taunt();
        yield return new WaitForSeconds(1.2f);

        _playerInput.enabled = true;
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

        // 전투가 끝났으므로 문을 엽니다.
        DoorControl(false);
        _playerInput.enabled = true;
    }

    public void GetDeadLocation(Vector3 location)
    {
        _deadLocation = location;
    }

}
