using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using JetBrains.Annotations;

public class ItemManager : Singleton<ItemManager>
{
    private Dictionary<ItemType, Dictionary<int, Item>> _items;
    public Dictionary<string, Sprite> ItemSprites {get; set;}

     public override bool Initialize()
    {
        if (base.Initialize())
        {
            LoadItem();
            LoadSprites();
            LoadSaveData(GameManager.Instance.Data.Inventory);
            Debug.Log("아이템");
        }
        return true;
    }

    private void LoadItem()
    {
        TextAsset itemData_Json = Resources.Load<TextAsset>("Json/ItemData");
        ItemData[] itemDatas = JsonConvert.DeserializeObject<ItemDataArray>(itemData_Json.ToString()).ItemDatas;

        _items = new Dictionary<ItemType, Dictionary<int, Item>>();

        foreach(ItemData data in itemDatas)
        {
            if(!_items.ContainsKey(data.ItemType))
            {
                _items[data.ItemType] = new Dictionary<int, Item>();
            }

            _items[data.ItemType].Add(data.ID, new Item(data));
        }
    }

    private void LoadSprites()
    {
        string path = "Items/Images";
        ItemSprites = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);

        foreach(Sprite sprite in sprites)
        {
            ItemSprites.Add(sprite.name, sprite);
        }
    }

    private void LoadSaveData(List<InternalItemData> inventoryData)
    {
        if (inventoryData == null) return;

        foreach (var itemData in inventoryData)
        {
            if (_items.TryGetValue(itemData.ItemType, out Dictionary<int, Item> itemDict))
            {
                if (itemDict.TryGetValue(itemData.ID, out Item item))
                {
                    item.SetItemStock(itemData.Stock);
                }
            }
        }
    }

    public bool UseItem(ItemType itemType, int ID, int value)
    {
        Item currentItem = _items[itemType][ID];
        int leftValue = currentItem.Stock - value;

        if (leftValue >= 0)
        {
            currentItem.SetItemStock(leftValue);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddItem(ItemType itemType, int ID, int value = 1)
    {
        Item currentItem = _items[itemType][ID];
        int sumValue = currentItem.Stock + value;
        currentItem.SetItemStock(sumValue);

        if (GameManager.Instance.Data.Inventory == null)
        {
            GameManager.Instance.Data.Inventory = new List<InternalItemData>();
        }

        bool found = false;
        foreach (var itemData in GameManager.Instance.Data.Inventory)
        {
            if (itemData.ItemType == itemType && itemData.ID == ID)
            {
                itemData.Stock += value;
                found = true;
                break;
            }
        }

        if (!found)
        {
            GameManager.Instance.Data.Inventory.Add(new InternalItemData { ItemType = itemType, ID = ID, Stock = value });
        }

        if(itemType == ItemType.Skill)
        {
            GameManager.Instance.player.SetSkill();
        }
    }

    public Dictionary<int, Item> GetItemDict(ItemType itemType)
    {
        return _items[itemType];
    }

    public Sprite GetSprite(string itemName)
    {
        return ItemSprites[itemName];
    }

    public Sprite GetSprite(ItemType itemType, int ID)
    {
        string curName = _items[itemType][ID].ItemData.Name;
        return ItemSprites[curName];
    }

    public ItemData GetItemData(ItemType itemType, int ID)
    {
        return _items[itemType][ID].ItemData;
    }

    public bool HasItem(ItemType itemType, int ID)
    {
        return _items[itemType][ID].Stock != 0;
    }

    public int GetItemStock(ItemType itemType, int ID)
    {
        return _items[itemType][ID].Stock;
    }

    public Item GetItem(ItemType itemType, int ID)
    {
        // StockChanged 접근이 필요한 경우!!!!!
        return _items[itemType][ID];
    }
}

[System.Serializable]
public class ItemDataArray
{
    public ItemData[] ItemDatas;
}