using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public enum ItemType
{
    None,
    Gold,
    Material,
    Equipment,
    Map,
    Skill,
    Building,
    Portal,
    Quest,
    NPC
}

public class Item
{
    public ItemData ItemData {get; private set;}
    public int Stock {get; private set;}
    public event Action<int> OnStockChanged;

    public Item(ItemData itemData)
    {
        ItemData = itemData;
        Stock = 0;
    }

    public void SetItemStock(int value)
    {
        Stock = value;
        OnStockChanged?.Invoke(value);
    }
}

public class ItemData
{
    public ItemType ItemType {get; set; }
    public int ID {get; set; }
    public string Name {get; set; }
    public string Description { get; set; }
}

[Serializable]
public class InternalItemData
{
    public ItemType ItemType;
    public int ID;
    public int Stock;
}