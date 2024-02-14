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
