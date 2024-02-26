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
            ClosePopupSign();
        }
    }

    protected abstract void OpenPopupSign();

    protected abstract void ClosePopupSign();
}
