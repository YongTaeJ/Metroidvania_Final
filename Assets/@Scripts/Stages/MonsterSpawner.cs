using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject _monsterPrefabs;

    private GameObject _spawnedMonsters;

    private void SpawnMonster()
    {
        // 몬스터가 생성되어 있는지 확인

        // 몬스터가 생성되어 있지 않다면

        _spawnedMonsters = Instantiate(_monsterPrefabs, transform.position, Quaternion.identity);
    }

    private void DestroyMonster()
    {
        if (_spawnedMonsters != null) // 생성된 몬스터가 있다면
        {
            Destroy(_spawnedMonsters);
        }
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
            DestroyMonster();
        }
    }
}
