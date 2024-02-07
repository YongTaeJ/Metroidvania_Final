using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeleportTrap : MonoBehaviour
{
    // 플레이어를 순간이동 시키는 함정
    // Vector3 teleportPosition을 갖고 있어서
    // 플레이어가 닿는 순간 해당하는 위치로 순간이동 시키기
    // 만약 필요하면 지금처럼 UI에 텍스트 표시해주기 유지

    private TextMeshProUGUI _teleportText;
    [SerializeField]
    private Vector3 _teleportPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = _teleportPosition;
            GameManager.Instance.player.GetDamaged(2, transform);
            OpenTrapText();
        }
    }

    private void OpenTrapText()
    {
        _teleportText = UIManager.Instance.GetUI(PopupType.Chest).GetComponentInChildren<TextMeshProUGUI>();
        
        if (this.CompareTag("Mimic"))
        {
            _teleportText.text = "You just Actived Trap Card";
        }

        else if (this.CompareTag("Challenge"))
        {
            _teleportText.text = "You are not prepared!";
        }

        else if (this.CompareTag("TownPortal"))
        {
            _teleportText.text = "You enter dungeon";
        }
        else
        {
            _teleportText.text = "You are fallen. Try again";
        }
        UIManager.Instance.OpenPopupUI(PopupType.Chest);
        StartCoroutine(DeactiveText(2));
    }

    private IEnumerator DeactiveText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UIManager.Instance.ClosePopupUI(PopupType.Chest);
    }
}
