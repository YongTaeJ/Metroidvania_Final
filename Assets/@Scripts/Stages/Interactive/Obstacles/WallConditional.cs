using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallConditional : WallBase
{
    [SerializeField]
    private int _itemID;

    protected override void WallReact(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ItemManager.Instance.HasItem(ItemType.Equipment, _itemID))
                BreakWall();
        }
    }
}
