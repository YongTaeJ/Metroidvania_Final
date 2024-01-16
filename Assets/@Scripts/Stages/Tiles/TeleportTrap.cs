using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeleportTrap : MonoBehaviour
{
    // damage 주는 함정
    // 플레이어의 체력을 받아서 일정 수치를 깍아서 반환
    // 플레이어를 특정 위치로 순간이동 시킴

    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private TextMeshProUGUI _mimicText;
    [SerializeField]
    private Vector3 _teleportPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = _teleportPosition;

            if (this.CompareTag("Mimic"))
            {
                MimicText();
            }
        }
    }

    private void MimicText()
    {
        _mimicText.text = "You just Actived Trap Card";
        _panel.SetActive(true);

        StartCoroutine(DeactiveText(2));
    }

    private IEnumerator DeactiveText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _panel.SetActive(false);
    }
}
