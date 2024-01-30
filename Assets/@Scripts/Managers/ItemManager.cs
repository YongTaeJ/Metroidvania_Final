using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ItemManager : Singleton<ItemManager>
{
    private Dictionary<ItemType, Dictionary<int, Item>> _items;
    public Dictionary<string, Sprite> ItemSprites {get; set;}

     public override bool Initialize()
    {
        LoadItem();
        LoadSprites();
        return base.Initialize();
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
        // TODO => 이후 Saved Data를 기준으로 Stock 설정. 없으면 Stock 전부 0인거 하나 만들어주기
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
    }



    public Dictionary<int, Item> GetItemDict(ItemType itemType)
    {
        return _items[itemType];
    }

    public ItemData GetItemData(ItemType itemType, int ID)
    {
        return _items[itemType][ID].ItemData;
    }

    public Sprite GetSprite(string itemName)
    {
        return ItemSprites[itemName];
    }

    public bool HasItem(ItemType itemType, int ID)
    {
        return _items[itemType][ID].Stock != 0;
    }
}

[System.Serializable]
public class ItemDataArray
{
    public ItemData[] ItemDatas;
}