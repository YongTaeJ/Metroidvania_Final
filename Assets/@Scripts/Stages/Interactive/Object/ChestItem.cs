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

        // 원래 코드
        //if (_chestItem == ItemType.Gold)
        //{
        //    ItemManager.Instance.AddItem(_chestItem, _chestItemID, _chestGold);
        //}
        //else if (_chestItem == ItemType.Skill)
        //{
        //    ItemManager.Instance.AddItem(_chestItem, _chestItemID);
        //    GameManager.Instance.player.SetSkill();
        //}
        //else
        //{
        //    ItemManager.Instance.AddItem(_chestItem, _chestItemID);
        //}

        // 상자 위치를 드랍 위치로 사용합니다.
        Vector2 dropLocation = new Vector2(transform.position.x+.7f, transform.position.y+.7f);

        // 골드 드랍
        if (_chestItem == ItemType.Gold)
        {
            DropManager.Instance.DropCoin(_chestGold, dropLocation);
        }
        // 다른 아이템 드랍
        else
        {
            DropManager.Instance.DropItem(_chestItem, _chestItemID, dropLocation);
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
