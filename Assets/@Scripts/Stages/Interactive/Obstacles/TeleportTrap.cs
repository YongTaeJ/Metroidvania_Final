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
    [SerializeField]
    private int _teleportID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = _teleportPosition;
            SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("TeleportTrap"));
            OpenTrapText();
        }
    }

    private void OpenTrapText()
    {
        _teleportText = UIManager.Instance.GetUI(PopupType.AToolTip).GetComponentInChildren<TextMeshProUGUI>();
        TrapText();
        UIManager.Instance.OpenPopupUI(PopupType.AToolTip);
        StartCoroutine(DeactiveText(2));
    }

    private IEnumerator DeactiveText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UIManager.Instance.ClosePopupUI(PopupType.AToolTip);
    }

    private void TrapText()
    {
        if (_teleportID == 0)
        {
            _teleportText.text = "You are fallen\n\rtry again";
        }
        else if (_teleportID == 1)
        {
            _teleportText.text = "Worng path.\n\rCheck the Chandelier";
        }
    }
}
