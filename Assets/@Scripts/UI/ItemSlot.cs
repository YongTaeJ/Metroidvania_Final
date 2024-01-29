using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private TextMeshProUGUI _stockText;
    private Image _itemImage;
    private Item _item;
    public void Initialize(Item item)
    {
        _item = item;
        _stockText = transform.Find("StockText").GetComponent<TextMeshProUGUI>();
        _itemImage = transform.Find("ItemImage").GetComponent<Image>();

        _itemImage.sprite = ItemManager.Instance.GetSprite(item.ItemData.Name);
        _stockText.text = item.Stock.ToString();

        _item.OnStockChanged += RefreshStock;

        Button button = GetComponent<Button>();

        // TODO => 현재 가지고 있는 Data를 inform에 전달
    }

    private void OnEnable()
    {
        if(_item == null) return;

        _item.OnStockChanged -= RefreshStock;
        _item.OnStockChanged += RefreshStock;
        RefreshStock(_item.Stock);
    }

    private void OnDisable()
    {
        _item.OnStockChanged -= RefreshStock;
    }

    private void RefreshStock(int value)
    {
        _stockText.text = value.ToString();
    }
}