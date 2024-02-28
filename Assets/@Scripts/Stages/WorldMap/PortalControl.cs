using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalControl : MonoBehaviour
{
    [SerializeField] private int _portalIndex;
    [SerializeField] private int _portalPrice = 50;
    private Canvas _buyPortal;
    protected PlayerInput _playerInput;
    protected Canvas _press;


    private void Awake()
    {
        _buyPortal = MapManager.Instance.BuyPortal;
        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            if (ItemManager.Instance.HasItem(ItemType.Portal, _portalIndex))
            {
                animator.SetBool("IsActivate", true);
            }
        }
        _press = GetComponentInChildren<Canvas>(true);
        if (_press != null) _press.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInput = collision.GetComponent<PlayerInput>();
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction += UsePortal;
            //UIManager.Instance.OpenPopupUI(PopupType.Interact);
            if (_press != null) _press.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction -= UsePortal;
            //UIManager.Instance.ClosePopupUI(PopupType.Interact);
            if (_press != null) _press.gameObject.SetActive(false);
        }
    }

    private void UsePortal()
    {
        MapTeleport mapTeleport = MapManager.Instance.GetComponentInChildren<MapTeleport>(true);

        if (mapTeleport != null)
        {
            mapTeleport.UpdatePortalButton();
        }
        if (ItemManager.Instance.HasItem(ItemType.Portal, _portalIndex))
        {
            GameManager.Instance.player.HP = GameManager.Instance.player.playerStatus.Stats[PlayerStatusType.HP];
            SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("WallHeal"), 0.2f);
        }            
        CheckHasPortal();
        //UIManager.Instance.ClosePopupUI(PopupType.Interact);
        if (_press != null) _press.gameObject.SetActive(false);
    }
    private void CheckHasPortal()
    {
        if (ItemManager.Instance.HasItem(ItemType.Portal, _portalIndex))
        {
            MapManager.Instance.OpenPortalMap();
            Animator animator = GetComponent<Animator>();

            if (animator != null)
            {
                animator.SetBool("IsActivate", true);
            }
        }
        else
        {
            MapManager.Instance.ActivateBuyPortalUI(_portalIndex, _portalPrice);
        }
    }
    public void PopupBuyPortal()
    {
        _buyPortal.gameObject.SetActive(true);
    }
}
