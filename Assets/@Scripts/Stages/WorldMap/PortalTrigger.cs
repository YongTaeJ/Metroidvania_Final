using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField]
    private Canvas _checkCanvas;
    [SerializeField]
    private int _portalIndex;
    [SerializeField]
    private int _portalPrice = 50;
    protected PlayerInput _playerInput;

    private void Awake()
    {
        _checkCanvas = MapManager.Instance.CheckCanvas;
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
        _checkCanvas.gameObject.SetActive(true);
    }

    private void BuyPortal()
    {
        ItemManager.Instance.AddItem(ItemType.Portal, _portalIndex);
        ItemManager.Instance.UseItem(ItemType.Gold, 0, _portalPrice);
    }

    public void ClickYes()
    {
        _checkCanvas.gameObject.SetActive(false);
        BuyPortal();
        Debug.Log(ItemManager.Instance.HasItem(ItemType.Portal, _portalIndex));
    }
    public void ClickNo()
    {
        _checkCanvas.gameObject.SetActive(false);
        Debug.Log("Cancel");
    }
}
