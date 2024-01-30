using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTeleport : MonoBehaviour
{
    // UI의 버튼을 누르면 해당 위치로 미니맵을 이동(마을은 이동 x)
    // 해당 위치의 대한 내용을 팝업으로 띄우면서 확인창(팝업창 하나 더 추가)


    // 순간이동 위치 - 마을, 포탈1(시작부분), 포탈2(보스방 앞), 포탈3(미니보스방)
    // 포탈4(TD부분), 포탈5(Stage 2 입구), 포탈6(Stage 3 입구)

    [SerializeField] private GameObject _checkCanvas;

    private void Awake()
    {
        _checkCanvas.SetActive(false);
    }

    public void ClickTeleport()
    {
        // 누른 버튼의 데이터 확인하여 해당 위치로 미니맵 이동
        // 해당 데이터의 위치에 대한 내용("마을"로 이동하시겠습니까, "포탈1"로 이동하시겠습니까)
        // 으로 팝업창 띄우고 만들기

        ToggleTeleport();
    }

    public void ToggleTeleport()
    {
        _checkCanvas.SetActive(true);
    }

    public void ClickYes()
    {
        // 순간이동 구현 부분
        _checkCanvas.SetActive(false);

    }

    public void ClickNo()
    {
        _checkCanvas.SetActive(false);
    }
}
