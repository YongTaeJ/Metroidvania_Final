using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    private Item _item;
    private TextMeshProUGUI _stockText;
    private Image _itemImage;

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(ItemType type, int ID)
    {
        gameObject.SetActive(true);
        
        _item = ItemManager.Instance.GetItem(type, ID);

        _itemImage = transform.Find("ItemImage").GetComponent<Image>();
        _stockText = transform.Find("StockText").GetComponent<TextMeshProUGUI>();

        _itemImage.sprite = ItemManager.Instance.GetSprite(_item.ItemData.Name);
        _stockText.text = _item.Stock.ToString();
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
        if (_item == null) return;

        _item.OnStockChanged -= RefreshStock;
    }

    private void RefreshStock(int value)
    {
        _stockText.text = value.ToString();
    }
}
