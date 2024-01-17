using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using JetBrains.Annotations;
using System.Threading;

public class DropManager : Singleton<DropManager>
{
    #region Fields
    public TextAsset itemTable_Json;
    private DropTableArray _dropTables;
    private Dictionary<string, GameObject> _itemPrefabs;
    private List<GameObject> _coinPrefabs;
    #endregion

    public override bool Initialize()
    {
        itemTable_Json = Resources.Load<TextAsset>("Json/DropTable");
        _dropTables = JsonConvert.DeserializeObject<DropTableArray>(itemTable_Json.ToString());

        InitPrefabs();

        return base.Initialize();
    }

    private void InitPrefabs()
    {
        _itemPrefabs = new Dictionary<string, GameObject>();
        _coinPrefabs = new List<GameObject>();

        // TODO => 폴더 내 데이터를 자동으로 처리하고, GameObject 이름을 Key값으로 받도록 수정
        _itemPrefabs.Add("Wood", Resources.Load<GameObject>("Items/Wood"));
        _itemPrefabs.Add("Brick", Resources.Load<GameObject>("Items/Brick"));

        _coinPrefabs.Add(Resources.Load<GameObject>("Items/Coin1"));
        _coinPrefabs.Add(Resources.Load<GameObject>("Items/Coin10"));
        _coinPrefabs.Add(Resources.Load<GameObject>("Items/Coin100"));
    }

    private void DropCoin(int value, Vector2 location)
    {
        int index = 0;
        while(value != 0)
        {
            // 1000원 이상은 작동 X
            if(index == 3) break;

            int count = value % 10;
            value /= 10;
            for(int i=0; i < count ; i++)
            {
                GameObject item = Instantiate(_coinPrefabs[index]);
                item.transform.localPosition = location;
            }
            index++;
        }
    }

    public void DropItem(int index, Vector2 location)
    {
        
        DropTable currentTable = _dropTables.DropTables[index];

        int goldValue = Random.Range(currentTable.MinGold, currentTable.MaxGold);
        DropCoin(goldValue, location);

        int chance = Random.Range(0,100);

        if(chance < currentTable.DropRate)
        {
            GameObject item = Instantiate(_itemPrefabs[currentTable.ItemName]);
            item.transform.localPosition = location;
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
    public string ItemName;
    public int DropRate;
}