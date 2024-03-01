using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BossRoom : MonoBehaviour
{
    #region variables
    [SerializeField] private int _bossItemID;
    protected ChatBoxUI _chatBoxUI;
    protected PlayerInput _playerInput;
    protected PlayerInputController _playerController;
    protected GameObject _leftWall;
    protected GameObject _rightWall;
    protected Transform _bossLocation;
    protected Vector3 _deadLocation;
    protected int _dropTableIndex;
    protected GameObject _currentBoss;
    private bool _isProgress;

    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        _isProgress = false;
        _chatBoxUI = UIManager.Instance.GetUI(PopupType.ChatBox).GetComponent<ChatBoxUI>();
        _leftWall = transform.Find("LeftWall").gameObject;
        _rightWall = transform.Find("RightWall").gameObject;
        _bossLocation = transform.Find("BossLocation");

        _leftWall.SetActive(false);
        _rightWall.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_isProgress && IsCleared() && other.CompareTag("Player"))
        {
            _isProgress = true;
            SetPlayerInput(other.gameObject);
            StartCoroutine(EnterBossRoom());
            GameManager.Instance.player.OnPlayerDead -= OnPlayerDead;
            GameManager.Instance.player.OnPlayerDead += OnPlayerDead;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(_currentBoss != null)
            {
                Destroy(_currentBoss);
                _currentBoss = null;
            }
        }
    }
    #endregion

    protected void DoorControl(bool isOpen)
    {
        // TODO => 문 닫힘 애니메이션을 추가해야 할 수도
        _leftWall.SetActive(isOpen);
        _rightWall.SetActive(isOpen);
    }

    private void SetPlayerInput(GameObject obj)
    {
        _playerInput = obj.GetComponent<PlayerInput>();
        _playerController = obj.GetComponent<PlayerInputController>();
    }

    private bool IsCleared()
    {
        return !ItemManager.Instance.HasItem(ItemType.Boss, _bossItemID);
    }

    public virtual void OnBossDead()
    {
        ItemManager.Instance.AddItem(ItemType.Boss, _bossItemID);
        DoorControl(false);
        StartCoroutine(EndBattle());
    }

    protected virtual void OnPlayerDead()
    {
        GameManager.Instance.player.OnPlayerDead -= OnPlayerDead;
        _isProgress = false;
        DoorControl(false);
    }

    public void GetDeadLocation(Vector3 location)
    {
        _deadLocation = location;
    }

    public void GetDropTableIndex(int ID)
    {
        _dropTableIndex = ID;
    }

    protected void PlayBossBGM()
    {
        BGM.Instance.Stop();
        BGM.Instance.Play("Boss", true);
    }

    protected abstract IEnumerator EnterBossRoom();
    protected abstract IEnumerator EndBattle();
    protected abstract void PlayStageBGM();
}
