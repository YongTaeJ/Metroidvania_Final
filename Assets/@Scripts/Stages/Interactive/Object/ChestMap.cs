using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMap : ChestBase
{
    [SerializeField] private MapData _mapData;
    [SerializeField] private int _mapDataLimit;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    protected override void ChestText()
    {
        base.ChestText();
        _chestText.text = "You acquired map data";
        _mapData._curMapData = 3;
        _mapData.UpdateMapData(_mapData._curMapData);
    }
}
