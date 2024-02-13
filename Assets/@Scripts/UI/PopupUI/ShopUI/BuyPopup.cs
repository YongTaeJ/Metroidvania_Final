using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyPopup : MonoBehaviour
{
    #region variables
    private InternalItemData _currentGoods;
    private Image _itemImage;
    private TMP_Text _itemName;
    private TMP_Text _itemCost;
    private QuantitySetter _quantitySetter;
    private Button _buyButton;
    private Button _exitButton;
    #endregion

    public void Initialize()
    {
        Transform Panel = transform.Find("Panel");

        _itemImage = Panel.Find("ItemImage").GetComponent<Image>();
        _itemName = Panel.Find("ItemName").GetComponent<TMP_Text>();
        _itemCost = Panel.Find("ItemCost").GetComponent<TMP_Text>();
        _quantitySetter = Panel.Find("QuantitySetter").GetComponent<QuantitySetter>();
        _buyButton = Panel.Find("BuyButton").GetComponent<Button>();
        _exitButton = Panel.Find("ExitButton").GetComponent<Button>();

        _quantitySetter.Initialize();

        _buyButton.onClick.AddListener(OnClickBuyButton);
        _exitButton.onClick.AddListener( () => { gameObject.SetActive(false); } );
    }

    public void SetPopupData(InternalItemData data)
    {
        gameObject.SetActive(true);
        _currentGoods = data;
        _itemImage.sprite = ItemManager.Instance.GetSprite(data.ItemType, data.ID);
        _itemName.text = ItemManager.Instance.GetItemData(data.ItemType, data.ID).Name ;
        _itemCost.text = data.Stock.ToString();
        _quantitySetter.ResetQuantity();
    }

    public void OnClickBuyButton()
    {
        gameObject.SetActive(false);
        int quantity = _quantitySetter.GetQuantity();
        int cost = _currentGoods.Stock * quantity;

        if(ItemManager.Instance.UseItem(ItemType.Gold, 0, cost))
        {
            // TODO => 구매 완료 팝업같은거?
            ItemManager.Instance.AddItem(_currentGoods.ItemType, _currentGoods.ID, quantity);
        }
        else
        {
            // TODO => 구매 안됨 팝업같은거?
        }
    }


}
