using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalTrigger : MonoBehaviour
{
    public bool CanUsePortal = false;
    protected PlayerInput _playerInput;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInput = collision.GetComponent<PlayerInput>();
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction += UsePortal;
            UIManager.Instance.OpenPopupUI(PopupType.Interact);

            MapManager.Instance._portalTrigger = this;
            CanUsePortal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction -= UsePortal;
            UIManager.Instance.ClosePopupUI(PopupType.Interact);

            MapManager.Instance._portalTrigger = null;
            CanUsePortal = false;
        }
    }

    private void UsePortal()
    {
        MapManager.Instance.OpenLargeMap();
        UIManager.Instance.ClosePopupUI(PopupType.Interact);
    }
}
