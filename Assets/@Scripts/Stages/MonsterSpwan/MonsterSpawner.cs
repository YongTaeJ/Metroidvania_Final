using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterSpawner : MonoBehaviour
{
    public MonsterSpawnData _spawnData;

    private GameObject[] _monsterPrefabs;
    private List<GameObject> _spawnedMonsters = new List<GameObject>();
    private Transform _enemiesParent;

    private void Awake()
    {
        _monsterPrefabs = Resources.LoadAll<GameObject>("Enemies/Monsters");
        _monsterPrefabs = _monsterPrefabs.OrderBy(prefab => prefab.name).ToArray();
        _enemiesParent = transform.parent.parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnMonster();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DestroyAllMonsters();
        }
    }

    private void SpawnMonster()
    {
        foreach (var spawnInfo in _spawnData.spawnInfos)
        {
            foreach (var position in spawnInfo.spawnPositions)
            {

                GameObject _spawnedMonster = Instantiate(spawnInfo.monsterPrefab, position, Quaternion.identity, _enemiesParent);
                _spawnedMonsters.Add(_spawnedMonster);
            }
        }
    }

    private void DestroyAllMonsters()
    {
        if (_spawnedMonsters != null)
        {
            foreach (GameObject monster in _spawnedMonsters)
            {
                Destroy(monster);
            }
            _spawnedMonsters.Clear();
        }
    }

    private void OldData()
    {
        // 리스트를 이용한 SpawnMonster 함수
        GameObject selectedMonster = _monsterPrefabs[0];
        GameObject spawnedMonster = Instantiate(selectedMonster, _enemiesParent);
        spawnedMonster.transform.localPosition = new Vector3(30, 4, 0);
        _spawnedMonsters.Add(spawnedMonster);

    }
}
