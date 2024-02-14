using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMap : ChestBase
{
    [SerializeField] private MapData _mapData;
    [SerializeField] private int _mapDataLimit;

    private int _curMapNumber;

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
        _mapData = MapManager.Instance.GetComponent<MapData>();
        _curMapNumber = ItemManager.Instance.GetItemStock(ItemType.Map, 0);

        if (_curMapNumber < _mapDataLimit)
        {
            ItemManager.Instance.AddItem(ItemType.Map, 0);
        }
    }

    protected override void ChestText()
    {
        base.ChestText();
        _chestText.text = "You acquired map data";
    }
}
