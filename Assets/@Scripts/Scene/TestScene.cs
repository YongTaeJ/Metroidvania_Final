using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : Singleton<TestScene>
{
    protected override void Start()
    {
        base.Start();
        
        SOManager.Instance.Initialize();
        ResourceManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        PoolManager.Instance.Initialize();
        MonsterPoolManager.Instance.Initialize();
        ItemManager.Instance.Initialize();
        GameManager.Instance.Initialize();
        EnemyDataManager.Instance.Initialize();
        DropManager.Instance.Initialize();
        ChatManager.Instance.Initialize();
        CameraManager.Instance.Initialize();
    }
}
