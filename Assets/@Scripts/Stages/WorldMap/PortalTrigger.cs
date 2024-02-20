using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField]
    private Canvas _buyPortal;
    [SerializeField]
    private int _portalIndex;
    [SerializeField]
    private int _portalPrice = 50;

    public int PortalIndex
    {
        get { return _portalIndex; }
    }

    private void Awake()
    {
        _buyPortal = MapManager.Instance.BuyPortal;
    }

    public void PopupBuyPortal()
    {
        _buyPortal.gameObject.SetActive(true);
    }

    private void BuyPortal()
    {
        ItemManager.Instance.UseItem(ItemType.Gold, 0, _portalPrice);
        ItemManager.Instance.AddItem(ItemType.Portal, _portalIndex);
        Debug.Log("BuyPortal");
        Debug.Log("UseGold");
    }

    public void ClickYes()
    {
        _buyPortal.gameObject.SetActive(false);
        BuyPortal();
    }
    public void ClickNo()
    {
        _buyPortal.gameObject.SetActive(false);
    }
}
