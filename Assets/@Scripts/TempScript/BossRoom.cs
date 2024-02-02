using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossRoom : MonoBehaviour
{
    private int _chatID;
    private bool _isEntered;
    private VHEntrySet _VHEntrySet;
    private Collider2D _leftWall;
    private Collider2D _rightWall;
    private Transform _bossLocation;

    private void Awake()
    {
        _leftWall = transform.Find("LeftWall").GetComponent<Collider2D>();
        _rightWall = transform.Find("RightWall").GetComponent<Collider2D>();
        _bossLocation = transform.Find("BossLocation");
        _chatID = 0;
        _isEntered = false;
    }

    private IEnumerator BossBattle()
    {
        var inputSystem = GameManager.Instance.player.GetComponent<PlayerInput>();
        inputSystem.enabled = false;

        // TODO => 카메라 처리는 일단 나중에 
        _VHEntrySet = Instantiate
        (Resources.Load<GameObject>("Enemies/Bosses/Auxiliary/VillageHead_EntrySet"), _bossLocation.position, Quaternion.identity)
        .GetComponent<VHEntrySet>();

        _VHEntrySet.Jump();
        yield return new WaitForSeconds(2.5f);

        yield return StartCoroutine(ChatManager.Instance.StartChatting(_chatID));

        _VHEntrySet.Taunt();
        yield return new WaitForSeconds(1.2f);

        _leftWall.enabled = true;
        _rightWall.enabled = true;
        inputSystem.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !_isEntered)
        {
            _isEntered = true;
            StartCoroutine(BossBattle());
        }
    }

    public void OpenDoor()
    {
        _leftWall.enabled = false;
        _rightWall.enabled = false;
    }
}
