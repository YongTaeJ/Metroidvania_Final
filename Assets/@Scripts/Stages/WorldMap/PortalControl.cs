using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalControl : MonoBehaviour
{
    private int _portalIndex;
    private PortalTrigger _portalTrigger;
    protected PlayerInput _playerInput;

    private void Awake()
    {
        _portalTrigger = GetComponent<PortalTrigger>();
        if (_portalTrigger != null)
        {
            _portalIndex = _portalTrigger.PortalIndex;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInput = collision.GetComponent<PlayerInput>();
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction += UsePortal;
            UIManager.Instance.OpenPopupUI(PopupType.Interact);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction -= UsePortal;
            UIManager.Instance.ClosePopupUI(PopupType.Interact);
        }
    }

    private void UsePortal()
    {
        CheckHasPortal();
        UIManager.Instance.ClosePopupUI(PopupType.Interact);
    }
    private void CheckHasPortal()
    {
        if (ItemManager.Instance.HasItem(ItemType.Portal, _portalIndex))
        {
            MapManager.Instance.OpenPortalMap();
        }
        else
        {
            _portalTrigger.PopupBuyPortal();
        }
    }

    public void ClosePortalMap()
    {
        MapManager.Instance.CloseLargeMap();
    }
}
