using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField]
    private bool _isOpened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isOpened = true;
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        this.gameObject.SetActive(false);
        // 추가로 애니메이션이나 동작을 넣을 수 있게 함수로 설정
    }
}
