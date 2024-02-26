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
            SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("TeleportTrap"));
            OpenTrapText();
        }
    }

    private void OpenTrapText()
    {
        _teleportText = UIManager.Instance.GetUI(PopupType.ToolTip).GetComponentInChildren<TextMeshProUGUI>();
        _teleportText.text = "You are fallen\n\rtry again";
        UIManager.Instance.OpenPopupUI(PopupType.ToolTip);
        StartCoroutine(DeactiveText(2));
    }

    private IEnumerator DeactiveText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UIManager.Instance.ClosePopupUI(PopupType.ToolTip);
    }
}
