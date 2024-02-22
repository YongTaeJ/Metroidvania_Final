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

            // 새 게임 시작 시 랜덤 정수 생성
            _data.randomUniqueNumber = UnityEngine.Random.Range(0, 2147483647);

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
        if (data != null)
        {
            this._data = data;

            //매니저 초기화 상태에서 Null임 메인 씬 관리할때 수정해야할 듯
            // 임시로 플레이어 Awake에서 데이터 받아서 이동
            Debug.Log($"Player 객체 상태: {(player != null ? "Initialized" : "Null")}");

            if (player != null)
            {
                Vector3 playerPosition = new Vector3(data.playerPositionX, data.playerPositionY, data.playerPositionZ);
                player.transform.position = playerPosition;
                Debug.Log("위치 로드");
            }
            else
            {
                Debug.Log("Player 객체가 Null입니다. 위치를 로드할 수 없습니다.");
            }
        }

        return true;
    }

    public void DeleteSaveFile()
    {
        File.Delete(_dataPath);
        Debug.Log("저장된 데이터 삭제");
    }
}
