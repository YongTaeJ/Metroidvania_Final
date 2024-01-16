using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyDataManager : Singleton<EnemyDataManager>
{
    // TODO => 데이터 관리 형태를 고민할 필요가 있어보임.
    public TextAsset enemyData_Json;
    private EnemyDataArray _enemyData;

    public override bool Initialize()
    {
        enemyData_Json = Resources.Load<TextAsset>("Json/EnemyData");
        _enemyData = JsonConvert.DeserializeObject<EnemyDataArray>(enemyData_Json.ToString());

        return base.Initialize();
    }

    public EnemyData GetEnemyData(int index)
    {
        return _enemyData.EnemyDatas[index];
    }
}

[System.Serializable]
public class EnemyDataArray
{
    public EnemyData[] EnemyDatas;
}

[System.Serializable]
public class EnemyData
{
    public string Name {get; set;}
    public int PrefabID {get; set;}
    public int HP { get; set; }
    public float Damage {get; set;}
    public float Speed {get; set;}
    public float AttackDistance { get; set; }
    public int HitEndurance {get; set;}
    public int DropTableIndex { get; set; }
}