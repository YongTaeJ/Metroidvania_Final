using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPortal : MonoBehaviour
{
    [SerializeField] private GameObject _needGold;
    [SerializeField] private GameObject _boughtPortal;

    public GameObject BuyPortalUI;
    private int _portalIndex;
    private int _portalPrice;


    public void ActivateUI(int index, int portalPrice)
    {
        _portalIndex = index;
        _portalPrice = portalPrice;
        BuyPortalUI.SetActive(true);
    }

    /// <summary>
    /// 포탈 구매버튼 클릭 버튼
    /// </summary>
    public void ClickYes()
    {
        if (ItemManager.Instance.UseItem(ItemType.Gold, 0, _portalPrice))
        {
            ItemManager.Instance.AddItem(ItemType.Portal, _portalIndex);

            // 포탈 구매해야 마을 포탈을 사용할 수 있게 하는 경우
            //if (!ItemManager.Instance.HasItem(ItemType.Portal, 0))
            //{
            //    ItemManager.Instance.AddItem(ItemType.Portal, 0);
            //}
            GameManager.Instance.SaveGame();
            _boughtPortal.gameObject.SetActive(true);
        }
        else
        {
            _needGold.gameObject.SetActive(true);
        }
    }

    public void ClickNo()
    {
        BuyPortalUI.SetActive(false);
    }

    public void ClickCheck()
    {
        BuyPortalUI.SetActive(false);
        _needGold.gameObject.SetActive(false);
        _boughtPortal.gameObject.SetActive(false);
    }
}
