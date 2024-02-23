using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public GameData Data => _data;
    private GameData _data = new();
    private string _dataPath;
    public Player player {  get; set; }



    public override bool Initialize()
    {
        Application.targetFrameRate = 60;
        _dataPath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log(Application.persistentDataPath);
        if (!LoadGame())
        {
            // Save Data가 없는경우.
            // TODO: 초기화 작업 추가
            InitItems();

            // 새 게임 시작 시 랜덤 정수 생성
            _data.randomUniqueNumber = UnityEngine.Random.Range(0, 2147483647);
        }
        return base.Initialize();
    }


    public void SaveGame()
    {
        Debug.Log("세이브");
        string jsonStr = JsonConvert.SerializeObject(_data);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonStr);
        string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(_dataPath, code);
    }

    public bool LoadGame()
    {
        if (!File.Exists(_dataPath)) return false;

        string code = File.ReadAllText(_dataPath);
        byte[] bytes = System.Convert.FromBase64String(code);
        string jsonStr = System.Text.Encoding.UTF8.GetString(bytes);

        GameData data = JsonConvert.DeserializeObject<GameData>(jsonStr);
        if (data != null)
        {
            this._data = data;
        }

        return true;
    }

    public void DeleteSaveFile()
    {
        File.Delete(_dataPath);
        Debug.Log("저장된 데이터 삭제");
    }

    private void InitItems()
    {
        ItemManager.Instance.AddItem(ItemType.NPC, 0);
        ItemManager.Instance.AddItem(ItemType.NPC, 5);
    }
}
