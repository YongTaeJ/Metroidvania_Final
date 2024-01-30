using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    // 체력 시스템을 가져와서 체력을 1로
    // 플레이어의 공격을 감지
    // 플레이어의 공격을 받으면 체력이 0되면서 아이템을 드랍 혹은 바로 획득

    // 몬스터와 겹치는 부분이 많으므로 이후에 몬스터의 스크립트를 상속받아서 적용하는 방식으로

    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private TextMeshProUGUI _chestText;
    [SerializeField]
    private MapData _mapdata;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _mapdata._curMapData++;
            _chestText.text = "You acquired map data";
            _panel.SetActive(true);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _panel.SetActive(false);
            GameObject.Destroy(gameObject);
        }
    }

    private void ChestText()
    {
        _chestText.text = "You opened chest\r\nYou got the Dash!";
        _panel.SetActive(true);
    }
}
