using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _checkCanvas;
    [SerializeField]
    private int _portalIndex;
    [SerializeField]
    private int _portalPrice = 50;
    protected PlayerInput _playerInput;

    private void Awake()
    {
        _checkCanvas.SetActive(false);
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
            MapManager.Instance.OpenLargeMap();
        }
        else
        {
            CheckBuyPortal();
            Debug.Log("BUY PORTAL");
        }
    }

    private void CheckBuyPortal()
    {
        _checkCanvas.SetActive(true);
    }

    private void BuyPortal()
    {
        ItemManager.Instance.AddItem(ItemType.Portal, _portalIndex);
        ItemManager.Instance.UseItem(ItemType.Gold, 0, _portalPrice);
    }

    public void ClickYes()
    {
        _checkCanvas.SetActive(false);
        BuyPortal();
        Debug.Log(ItemManager.Instance.HasItem(ItemType.Portal, _portalIndex));
    }
    public void ClickNo()
    {
        _checkCanvas.SetActive(false);
        Debug.Log("Cancel");
    }
}
