using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMap : ChestBase
{
    [SerializeField] private MapData _mapdata;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        _mapdata._curMapData++;
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

}
