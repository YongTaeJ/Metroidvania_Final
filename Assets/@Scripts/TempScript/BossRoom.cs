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

        if(_count == 0)
        {
            _count++;
            DoorControl(true);
            _input.enabled = false;

            _controller.Move(Vector2.right);
            yield return new WaitForSeconds(0.3f);
            _controller.Move(Vector2.zero);

            _VHEntrySet = Instantiate
            (Resources.Load<GameObject>("Enemies/Bosses/VillageHead/VillageHead_EntrySet"), _bossLocation.position, Quaternion.identity)
            .GetComponent<VHEntrySet>();

            _VHEntrySet.Jump();
            yield return new WaitForSeconds(2.5f);

            _chatBoxUI.ActiveUI(true);
            chatDatas = ChatManager.Instance.GetChatData(_chatID_encounter);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

            _VHEntrySet.Taunt();
            yield return new WaitForSeconds(1.2f);
        }
        else if(_count == 1)
        {
            StartInteract(_input);

            // TODO => 보스 위치 알아낸 다음 거따 소환해야됨
            VHEndSet endSet = Instantiate(Resources.Load("Enemies/Bosses/VillageHead/VHEndSet"), _deadPosition, Quaternion.identity).GetComponent<VHEndSet>();

            chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_0);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

            endSet.TriggerEffect();

            chatDatas = ChatManager.Instance.GetChatData(_chatID_defeat_1);
            yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));

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
        _chatBoxUI.ActiveUI(true);
    }

    protected void EndInteract(PlayerInput input)
    {
        _chatBoxUI.ActiveUI(false);
        input.enabled = true;
    }
}
