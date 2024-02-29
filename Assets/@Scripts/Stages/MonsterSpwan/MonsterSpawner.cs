using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterSpawner : MonoBehaviour
{
    public MonsterSpawnData _spawnData;

    private GameObject[] _monsterPrefabs;
    private List<GameObject> _spawnedMonsters = new List<GameObject>();
    public bool IsClear = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IsClear)
        {
            SpawnMonster();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ClearCheck();
            Invoke("DestroyAllMonsters", 0.1f);
        }
    }

    private void SpawnMonster()
    {
        foreach (var spawnInfo in _spawnData.spawnInfos)
        {
            foreach (var position in spawnInfo.spawnPositions)
            {
                GameObject _spawnedMonster = MonsterPoolManager.Instance.SpawnFromPool(spawnInfo.monsterPrefab.name, position, spawnInfo.monsterPrefab.transform.rotation);
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
                if (monster != null)
                {
                    monster.SetActive(false);
                }
            }
            _spawnedMonsters.Clear();
        }
    }

    private void ClearCheck()
    {
        int remainMonster = _spawnedMonsters.Count(monster => monster.activeSelf);

        if ((float)remainMonster <= (_spawnedMonsters.Count / 3.0f))
        {
            IsClear = true;
        }
    }

    //private void OldData()
    //{
    //    // 리스트를 이용한 SpawnMonster 함수
    //    GameObject selectedMonster = _monsterPrefabs[0];
    //    GameObject spawnedMonster = Instantiate(selectedMonster, _enemiesParent);
    //    spawnedMonster.transform.localPosition = new Vector3(30, 4, 0);
    //    _spawnedMonsters.Add(spawnedMonster);

    //}
}
