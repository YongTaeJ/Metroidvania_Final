using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : ChestBase
{
    // 골드나 아이템은 같은 방식으로 작동되게 할 것이므로 같은 ChestItem를 사용

    [SerializeField] private int _chestGold;
    [SerializeField] private ItemType _chestItem;
    [SerializeField] private int _chestItemID;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (_chestItem == ItemType.Gold)
        {
            // 마지막 150 부분에 골드량을 조절할 수 있게 변경하면 될듯
            ItemManager.Instance.AddItem(_chestItem, _chestItemID, 150);
        }
        else
        {
            ItemManager.Instance.AddItem(_chestItem, _chestItemID);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    protected override void ChestText()
    {
        ItemData _chestItemData = ItemManager.Instance.GetItemData(_chestItem, _chestItemID);
        string _chestItemName = _chestItemData.Name;
        _chestText.text = "You got " + _chestItemName;
        _panel.SetActive(true);
    }
}
