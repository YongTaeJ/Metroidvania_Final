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

    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private TextMeshProUGUI _teleportText;
    [SerializeField]
    private Vector3 _teleportPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = _teleportPosition;
            GameManager.Instance.player.GetDamaged(2, transform);

            if (this.CompareTag("Mimic"))
            {
                MimicText();
            }

            else if (this.CompareTag("Challenge"))
            {
                ChallengeText();
            }

            else if (this.CompareTag("TownPortal"))
            {
                TownPortalText();
            }
            else
            {
                FallText();
            }
        }
    }

    private void MimicText()
    {
        _teleportText.text = "You just Actived Trap Card";
        _panel.SetActive(true);

        StartCoroutine(DeactiveText(2));
    }
    private void ChallengeText()
    {
        _teleportText.text = "You are not prepared!";
        _panel.SetActive(true);

        StartCoroutine(DeactiveText(2));
    }

    private void FallText()
    {
        _teleportText.text = "You are fallen. Try again";
        _panel.SetActive(true);

        StartCoroutine(DeactiveText(2));
    }
    private void TownPortalText()
    {
        _teleportText.text = "You enter dungeon";
        _panel.SetActive(true);

        StartCoroutine(DeactiveText(4));
    }

    private IEnumerator DeactiveText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _panel.SetActive(false);
    }
}
