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

        if (item.ItemData.ItemType == ItemType.Skill || item.ItemData.ItemType == ItemType.Equipment)
        {
            _stockText.gameObject.SetActive(false);
        }

        _item.OnStockChanged += RefreshStock;

        Button button = GetComponent<Button>();
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

    public void CheckStock()
    {
        if(_item.Stock != 0) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
