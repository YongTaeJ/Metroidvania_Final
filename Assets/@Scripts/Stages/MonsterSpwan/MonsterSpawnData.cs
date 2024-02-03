using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSpawnData", menuName = "ScriptableObjects/MonsterSpawnData", order = 1)]
public class MonsterSpawnData : ScriptableObject
{
    [System.Serializable]
    public struct MonsterSpawnInfo
    {
        public GameObject monsterPrefab;
        public Vector3[] spawnPositions;
    }

    public MonsterSpawnInfo[] spawnInfos;
}
