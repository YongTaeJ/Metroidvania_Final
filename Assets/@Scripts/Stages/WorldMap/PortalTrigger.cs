using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private Canvas _buyPortal;
    [SerializeField] private int _portalIndex;
    [SerializeField] private int _portalPrice = 50;

    private void Awake()
    {
        _buyPortal = MapManager.Instance.BuyPortal;
    }

    public void PopupBuyPortal()
    {
        _buyPortal.gameObject.SetActive(true);
    }
    private void BuyPortal(int _portalIndex)
    {
        if (ItemManager.Instance.UseItem(ItemType.Gold, 0, _portalPrice))
        {
            ItemManager.Instance.AddItem(ItemType.Portal, _portalIndex);
            GameManager.Instance.SaveGame();
            Debug.Log("포탈 구매");
        }
        else
        {
            Debug.Log("골드 부족");
        }
    }
}
