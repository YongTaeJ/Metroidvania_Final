using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DisapearPlatform : MonoBehaviour
{
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
