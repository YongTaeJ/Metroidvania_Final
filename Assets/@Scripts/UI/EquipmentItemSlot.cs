using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemSlot : MonoBehaviour
{
    private Image _itemImage;
    private Item _item;
    public void Initialize(Item item)
    {
        _item = item;
        _itemImage = transform.Find("ItemImage").GetComponent<Image>();

        _itemImage.sprite = ItemManager.Instance.GetSprite(item.ItemData.Name);


        Button button = GetComponent<Button>();

    }
}
