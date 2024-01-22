using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyDataManager : Singleton<EnemyDataManager>
{
    #region Fields
    public TextAsset enemyData_Json;
    private EnemyDataArray _enemyData;
    #endregion

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
    public int Damage {get; set;}
    public float Speed {get; set;}
    public float AttackDistance { get; set; }
    public int HitEndurance {get; set;}
    public int DropTableIndex { get; set; }
}