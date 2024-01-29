using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DisapearPlatform : MonoBehaviour
{
    // 밟으면 사라지는 발판
    // 플레이어가 닿는 순간 사라지게 작동(함정 느낌)
    // 2초 후에 다시 생기게 만들어둠


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.gameObject.SetActive(false);
            Invoke("ActivePlatform", 2f);
        }
    }

    private void ActivePlatform()
    {
        transform.parent.gameObject.SetActive(true);
    }
}
