using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusInformPanel : MonoBehaviour
{
    private Image _itemImage;
    private TextMeshProUGUI _itemName;
    private TextMeshProUGUI _itemDescription;
    private TextMeshProUGUI _itemType;
    private TextMeshProUGUI _stock;

    
    private void Awake()
    {
        _itemImage = transform.Find("Image").GetComponent<Image>();
        _itemName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        _itemType = transform.Find("Type").GetComponent<TextMeshProUGUI>();
        _itemDescription = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        _stock = transform.Find("Stock").GetComponent<TextMeshProUGUI>();
    }

    public void SetInform(Item item)
    {
        _itemImage.sprite = ItemManager.Instance.GetSprite(item.ItemData.Name);
        _itemName.text = item.ItemData.Name;
        _itemType.text = item.ItemData.ItemType.ToString();
        _itemDescription.text = item.ItemData.Description;

        if(item.Stock != 0)
        {
            _stock.text = item.Stock.ToString() + "개 보유중";
        }
        else
        {
            _stock.text = "없음";
        }
    }
}