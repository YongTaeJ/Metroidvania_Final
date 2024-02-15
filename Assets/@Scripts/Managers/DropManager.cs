using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DropManager : Singleton<DropManager>
{
    #region Fields
    private DropTable[] _dropTables;
    private GameObject[] coinPrefabs;
    private GameObject _dropItem;
    #endregion

    public override bool Initialize()
    {
        TextAsset itemTable_Json = Resources.Load<TextAsset>("Json/DropTable");
        _dropTables = JsonConvert.DeserializeObject<DropTableArray>(itemTable_Json.ToString()).DropTables;
        
        _dropItem = Resources.Load<GameObject>("Items/DropItem");

        coinPrefabs = new GameObject[3];
        coinPrefabs[0] = Resources.Load<GameObject>("Items/Coin1");
        coinPrefabs[1] = Resources.Load<GameObject>("Items/Coin10");
        coinPrefabs[2] = Resources.Load<GameObject>("Items/Coin100");

        return base.Initialize();
    }

    private void DropCoin(int sumValue, Vector2 location)
    {
        // 현재 0~999원까지만 설계됨.
        int prefabindex = 0;
        int value = 1;
        while(sumValue != 0)
        {
            int remainder = sumValue % 10;
            sumValue /= 10;

            for(int i=0; i < remainder; i++)
            {
                var coin = PoolManager.Instance.Pop(coinPrefabs[prefabindex]);
                coin.GetComponent<DropItem>().Initialize(ItemType.Gold, 0, value);
                coin.transform.position = location;
            }
            prefabindex++;
            value *= 10;
        }
    }

    public void DropItem(int tableIndex, Vector2 dropLocation)
    {

        DropTable currentTable = _dropTables[tableIndex];

        int coinValue = Random.Range(currentTable.MinGold, currentTable.MaxGold+1);

        DropCoin(coinValue, dropLocation);

        int chance = Random.Range(0,100);

        if(chance < currentTable.DropRate)
        {
            // 현재 Material만 드랍되는 것으로 설계됨.
            ItemData data = ItemManager.Instance.GetItemData(ItemType.Material, currentTable.ItemID);
            GameObject dropitem = PoolManager.Instance.Pop(_dropItem);
            dropitem.transform.position = dropLocation;
            dropitem.GetComponent<DropItem>().Initialize(data.ItemType, data.ID, 1);
            dropitem.GetComponentInChildren<SpriteRenderer>().sprite = ItemManager.Instance.GetSprite(data.Name);
        }
    }
}

public class DropTableArray
{
    public DropTable[] DropTables;
}

public class DropTable
{
    public int Index;
    public int MinGold;
    public int MaxGold;
    public int ItemID;
    public int DropRate;
}