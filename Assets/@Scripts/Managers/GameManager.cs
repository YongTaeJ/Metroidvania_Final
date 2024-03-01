using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameData Data => _data;
    private GameData _data = new();
    private string _dataPath;
    public Player player {  get; set; }

    public override bool Initialize()
    {
        if(base.Initialize())
        {
            _dataPath = Application.persistentDataPath + "/SaveData.json";
            Debug.Log(Application.persistentDataPath);
            if (!IsFileExits())
            {
                InitFirstSettings();
            }
            else
            {
                LoadGame();
            }
        }
        return true;
    }


    public void SaveGame()
    {
        _data.Inventory = ConvertItemData();
        _data.PlayerStatusData = GameManager.Instance.player.playerStatus.GetSaveData();

        string jsonStr = JsonConvert.SerializeObject(_data);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonStr);
        string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(_dataPath, code);
    }
    
    private List<InternalItemData> ConvertItemData()
    {
        int limit = Enum.GetValues(typeof(ItemType)).Length;
        List<InternalItemData> Datas = new List<InternalItemData>();

        // 0은 None.
        for(int i=1; i < limit ; i++)
        {
            ItemType currentType = (ItemType) i ;
            Dictionary<int, Item> items = ItemManager.Instance.GetItemDict(currentType);

            foreach(Item item in items.Values)
            {
                Datas.Add(new InternalItemData(item)); 
            }
        }
        return Datas;
    }

    private void LoadGame()
    {
        string code = File.ReadAllText(_dataPath);
        byte[] bytes = System.Convert.FromBase64String(code);
        string jsonStr = System.Text.Encoding.UTF8.GetString(bytes);

        GameData data = JsonConvert.DeserializeObject<GameData>(jsonStr);
        if (data != null)
        {
            this._data = data;
        }

        ItemManager.Instance.LoadData(_data.Inventory);
    }

    public bool IsFileExits()
    {
        return File.Exists(_dataPath);
    }

    public void DeleteSaveFile()
    {
        // Temp Code.
        PlayerPrefs.DeleteAll();
        File.Delete(_dataPath);
        Debug.Log("저장된 데이터 삭제");
    }

    private void InitFirstSettings()
    {
        Debug.Log("초기세팅");
        ItemManager.Instance.AddItem(ItemType.NPC, 0);
        ItemManager.Instance.AddItem(ItemType.NPC, 5);
        _data.randomUniqueNumber = UnityEngine.Random.Range(0, 2147483647);
    }
}
