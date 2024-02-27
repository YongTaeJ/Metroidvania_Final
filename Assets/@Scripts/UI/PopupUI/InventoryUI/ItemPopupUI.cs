using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopupUI : MonoBehaviour
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

        Button button = transform.Find("ExitButton").GetComponent<Button>();
        button.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void SetInform(Item item)
    {
        gameObject.SetActive(true);
        
        _itemImage.sprite = ItemManager.Instance.GetSprite(item.ItemData.Name);
        _itemName.text = item.ItemData.NameKor;
        _itemType.text = item.ItemData.TypeKor;
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
