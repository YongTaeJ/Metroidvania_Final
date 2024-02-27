using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            _chestText.text = "상자에는 약간의 골드가 들어있었다";
        }
        else
        {
            ItemData _chestItemData = ItemManager.Instance.GetItemData(_chestItem, _chestItemID);
            string _chestItemName = _chestItemData.Name;
            // 아래를 descrition을 받아오는 방향으로 수정
            _chestText.text = "You got " + _chestItemName;
            
            if (_chestItem == ItemType.Skill || _chestItem == ItemType.Equipment)
            {
                Invoke("HelpText", 1.3f);
            }
        }
    }

    private void HelpText()
    {
        _chestText = UIManager.Instance.GetUI(PopupType.AToolTip).GetComponentInChildren<TextMeshProUGUI>();

        if (_chestItem == ItemType.Skill && _chestItemID == 0)
        {
            _chestText.text = "이제 A키를 눌러서\n\r오러 공격을 할 수 있다";
        }

        if (_chestItem == ItemType.Skill && _chestItemID == 1)
        {
            _chestText.text = "공중에서 ↓ 와 A 키를 누르면\n\r낙하 공격을 할 수 있다";
        }

        if (_chestItem == ItemType.Equipment && _chestItemID == 0)
        {
            _chestText.text = "이제 C키를 눌러서\n\r대쉬를 할 수 있다";
        }

        if (_chestItem == ItemType.Equipment && _chestItemID == 1)
        {
            _chestText.text = "이제 벽을 짚고 점프할 수 있다";
        }

        if (_chestItem == ItemType.Equipment && _chestItemID == 2)
        {
            _chestText.text = "이제 공중에서 점프를\n\r한 번 더 할 수 있다";
        }

        UIManager.Instance.OpenPopupUI(PopupType.AToolTip);
        StartCoroutine(CoHelpTextOff());
    }
    private IEnumerator CoHelpTextOff()
    {
        yield return new WaitForSeconds(1.4f);
        UIManager.Instance.ClosePopupUI(PopupType.AToolTip);
        GameObject.Destroy(gameObject);
    }
}
