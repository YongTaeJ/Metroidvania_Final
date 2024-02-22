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
    private QuantitySetter _quantitySetter;
    private ShoppingList _shoppingList;
    private Button _buyButton;
    private Button _exitButton;
    #endregion

    public void Initialize(ShopUI shopUI)
    {
        _itemImage = transform.Find("ItemImage").GetComponent<Image>();
        _itemName = transform.Find("ItemName").GetComponent<TMP_Text>();
        _quantitySetter = transform.Find("QuantitySetter").GetComponent<QuantitySetter>();
        _buyButton = transform.Find("BuyButton").GetComponent<Button>();
        _exitButton = transform.Find("ExitButton").GetComponent<Button>();

        _shoppingList = shopUI.ShoppingList;

        _quantitySetter.Initialize();

        _buyButton.onClick.AddListener(OnClickBuyButton);
        _exitButton.onClick.AddListener(ClosePopup);
    }

    public void SetPopupData(InternalItemData data)
    {
        gameObject.SetActive(true);
        _currentGoods = data;
        _itemImage.sprite = ItemManager.Instance.GetSprite(data.ItemType, data.ID);
        _itemName.text = ItemManager.Instance.GetItemData(data.ItemType, data.ID).Name ;
        _quantitySetter.ResetQuantity(data.Stock);
    }

    public void OnClickBuyButton()
    {
        ClosePopup();

        int cost = _quantitySetter.GetCost();
        int quantity = _quantitySetter.GetQuantity();

        if(ItemManager.Instance.UseItem(ItemType.Gold, 0, cost))
        {
            ItemManager.Instance.AddItem(_currentGoods.ItemType, _currentGoods.ID, quantity);
        }
    }

    public void ClosePopup()
    {
        _shoppingList.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


}
