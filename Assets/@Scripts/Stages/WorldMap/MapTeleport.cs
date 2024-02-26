using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapTeleport : MonoBehaviour
{
    // 포탈 위치
    // ID - 0 : Town - 마을
    // ID - 1 : Stair - Stage 1 보스방 앞
    // ID - 2 : Shelter - Stage 1 좌측상단 챌린지방
    // ID - 3 : Cathedral - Stage 2 입구

    [SerializeField] 
    private Canvas _checkCanvas;
    private int _selectedButtonIndex;
    private Camera _mapCamera;

    [SerializeField] private Button[] portalButtons;
    [SerializeField] private Vector3[] portalLocations;
    [SerializeField] private TextMeshProUGUI portalText;

    private Canvas _press;

    private void Awake()
    {
        _mapCamera = GetComponentInChildren<Camera>();

        for (int i = 0; i < portalButtons.Length; i++)
        {
            int index = i;
            portalButtons[i].onClick.AddListener(() => ClickTeleport(index));
        }

        UpdatePortalButton();
        //portalLocations[0] = new Vector3(224.5f, -.05f, 0);
        //portalLocations[1] = new Vector3(301, -188.5f, 0);
        //portalLocations[2] = new Vector3(210, 104.5f, 0);
        //portalLocations[3] = new Vector3(416.5f, -146.5f, 0);
    }

    public void ClickTeleport(int index)
    {
        _selectedButtonIndex = index;
        PortalText(index);
        _checkCanvas.gameObject.SetActive(true);

        if (index != 0)
        {
            MoveMapCamera(portalLocations[_selectedButtonIndex]);
        }
    }

    public void ClickYes()
    {
        _checkCanvas.gameObject.SetActive(false);
        Teleport(_selectedButtonIndex);
        MapManager.Instance.CloseLargeMap();
        //UIManager.Instance.OpenPopupUI(PopupType.Interact);
        if (_press != null) _press.gameObject.SetActive(true);
    }

    public void ClickNo()
    {
        _checkCanvas.gameObject.SetActive(false);
    }

    private void Teleport(int index)
    {
        MapManager.Instance.LoadImage(true);
       
        Vector3 newPosition = portalLocations[index];
        GameManager.Instance.player.transform.position = newPosition;

        BGMConttroll(index);

        // 새 위치 정보를 GameData에 저장합니다.
        GameManager.Instance.Data.playerPositionX = newPosition.x;
        GameManager.Instance.Data.playerPositionY = newPosition.y;
        GameManager.Instance.Data.playerPositionZ = newPosition.z;

        SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("Teleport"));
        GameManager.Instance.SaveGame();
    }

    private void PortalText(int index)
    {
        ItemData itemData = ItemManager.Instance.GetItemData(ItemType.Portal, index);
        string portalName = itemData.Name;
        if (index == 0)
        {
            portalText.text = "마을(으/로)\r\n" + "이동하시겠습니까?";
        }
        else
        {
            portalText.text = portalName + "(으/로)\r\n" + "이동하시겠습니까?";
        }
    }

    private void MoveMapCamera(Vector3 position)
    {
        _mapCamera.transform.position = new Vector3(position.x, position.y + 8, position.z - 10);
    }

    public void UpdatePortalButton()
    {
        for (int i = 0; i < portalButtons.Length; i++)
        {
            if (ItemManager.Instance.HasItem(ItemType.Portal, i))
            {
                portalButtons[i].gameObject.SetActive(true);
            }
            else
            {
                portalButtons[i].gameObject.SetActive(false);
            }
        }
    }
    public void ClosePortalMap()
    {
        MapManager.Instance.CloseLargeMap();
    }

    //임시용 BGM 컨트롤
    private void BGMConttroll(int index)
    {
        if (index == 0)
        {
            BGM.Instance.Stop();
            BGM.Instance.Play("Home", true);
        }
        else if(index == 1 || index == 2) 
        {
            BGM.Instance.Stop();
            BGM.Instance.Play("Stage1", true);
        }
        else if (index == 3)
        {
            BGM.Instance.Stop();
            BGM.Instance.Play("Stage2", true);
        }
    }
}
