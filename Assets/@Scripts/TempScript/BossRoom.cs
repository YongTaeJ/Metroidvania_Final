using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossRoom : MonoBehaviour
{

    #region variables
    protected ChatBoxUI _chatBoxUI;
    [SerializeField] private int _chatID_encounter;
    [SerializeField] private int _chatID_defeat_0;
    [SerializeField] private int _chatID_defeat_1;
    private VHEntrySet _VHEntrySet;
    private Collider2D _leftWall;
    private Collider2D _rightWall;
    private Transform _bossLocation;
    private PlayerInput _input;
    private PlayerInputController _controller;
    private Vector3 _deadPosition;
    private int _count;
    private bool _isEntered;
    #endregion


    private void Awake()
    {
        _chatBoxUI = UIManager.Instance.GetUI(PopupType.ChatBox).GetComponent<ChatBoxUI>();
        _leftWall = transform.Find("LeftWall").GetComponent<Collider2D>();
        _rightWall = transform.Find("RightWall").GetComponent<Collider2D>();
        _bossLocation = transform.Find("BossLocation");
        _isEntered = false;
        _count = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !_isEntered)
        {
            _isEntered = true;
            StartCoroutine(Interact(other.gameObject));
        }
    }

    public void DoorControl(bool isOpen)
    {
        // TODO => 문 닫힘 애니메이션을 추가해야 할 수도
        _leftWall.enabled = isOpen;
        _rightWall.enabled = isOpen;
    }

    public IEnumerator Interact(GameObject obj)
    {
        if(obj != null)
        {
            _input = obj.GetComponent<PlayerInput>();
            _controller = obj.GetComponent<PlayerInputController>();
        }

        List<(string, string)> chatDatas;

        // 들어갔을 때 발동하는 메서드입니다.
        // TODO => 처치하지 못하고 죽은 다음 다시 들어갔을 때 오류가 생길 수 있습니다.
        if(_count == 0)
        {
            _count++;

            // 컨트롤러를 끊고, 캐릭터가 적당한 위치까지 오도록 만듭니다.
            DoorControl(true);
            _input.enabled = false;
            _controller.Move(Vector2.right);
            yield return new WaitForSeconds(0.3f);
            _controller.Move(Vector2.zero);

            // 이벤트 전용 마을 촌장 게임 오브젝트를 생성하고, 조종하기 위해 참조를 할당합니다.
            _VHEntrySet = Instantiate
            (Resources.Load<GameObject>("Enemies/Bosses/VillageHead/VillageHead_EntrySet"), _bossLocation.position, Quaternion.identity)
            .GetComponent<VHEntrySet>();

            _VHEntrySet.Jump();
            yield return new WaitForSeconds(2.5f);

            chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

            _VHEntrySet.Taunt();
            yield return new WaitForSeconds(1.2f);
        }
        // 보스를 처치한 뒤 나타날 메서드입니다.
        else if(_count == 1)
        {
            StartInteract(_input);

            // 이벤트 전용 마을 촌장 게임 오브젝트를 생성하고, 조종하기 위해 참조를 할당합니다.
            VHEndSet endSet = Instantiate(Resources.Load("Enemies/Bosses/VillageHead/VHEndSet"), _deadPosition, Quaternion.identity).GetComponent<VHEndSet>();

            chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_0);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

            endSet.TriggerEffect();

            chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_1);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

            // 전투가 끝났으므로 문을 엽니다.
            // TODO => 플레이어 사망 시 문을 열 수 있도록 조정할 필요성이 있습니다.
            DoorControl(false);
        }
        else
        {
            Debug.Log("보스 인터렉션 예외 발생");
        }

        EndInteract(_input);
    }

    public void InvokeInteract(Vector3 position)
    {
        _deadPosition = position;
        StartCoroutine(Interact(null));
    }

    protected void StartInteract(PlayerInput input)
    {
        input.enabled = false;
    }

    protected void EndInteract(PlayerInput input)
    {
        input.enabled = true;
    }
}
