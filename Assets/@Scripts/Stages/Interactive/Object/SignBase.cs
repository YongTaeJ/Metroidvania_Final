using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SignBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenPopupSign();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 너무 빨리 없어지는 느낌이면 코루틴으로 지연하여 없어지게
            ClosePopupSign();
        }
    }

    protected abstract void OpenPopupSign();

    protected abstract void ClosePopupSign();
}
