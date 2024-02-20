using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalControl : MonoBehaviour
{
    [SerializeField] private int _portalIndex;
    [SerializeField] private int _portalPrice = 50;
    protected PlayerInput _playerInput;

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
            MapManager.Instance.ActivateBuyPortalUI(_portalIndex, _portalPrice);
        }
    }
    public void ClosePortalMap()
    {
        MapManager.Instance.CloseLargeMap();
    }
}
