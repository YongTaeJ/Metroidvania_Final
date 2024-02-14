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
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    protected override void OpenChest()
    {
        base.OpenChest();

        if (_chestItem == ItemType.Gold)
        {
            ItemManager.Instance.AddItem(_chestItem, _chestItemID, _chestGold);
        }
        else if (_chestItem == ItemType.Skill)
        {
            ItemManager.Instance.AddItem(_chestItem, _chestItemID);
            GameManager.Instance.player.SetSkill();
        }
        else
        {
            ItemManager.Instance.AddItem(_chestItem, _chestItemID);
        }
    }

    protected override void ChestText()
    {
        base.ChestText();
        if (_chestItem == ItemType.Gold)
        {
            _chestText.text = "You got " + _chestGold + " Gold";
        }
        else
        {
            ItemData _chestItemData = ItemManager.Instance.GetItemData(_chestItem, _chestItemID);
            string _chestItemName = _chestItemData.Name;
            _chestText.text = "You got " + _chestItemName;
        }
    }
}
