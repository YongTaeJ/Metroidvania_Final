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
            // 초기화 작업을 수행한 후 초기 데이터를 저장.
            // TODO: 초기화 작업 추가
            SaveGame();
        }
        return base.Initialize();
    }


    public void SaveGame()
    {
        string jsonStr = JsonConvert.SerializeObject(_data);
        File.WriteAllText(_dataPath, jsonStr);
    }

    public bool LoadGame()
    {
        if (!File.Exists(_dataPath)) return false;

        string file = File.ReadAllText(_dataPath);
        GameData data = JsonConvert.DeserializeObject<GameData>(file);
        if (data != null) this._data = data;

        return true;
    }
}
