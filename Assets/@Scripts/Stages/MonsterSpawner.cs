using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private GameObject _monsterPrefabs;
    private GameObject _spawnedMonsters;
    private GameObject _enemiesParent;
    private bool IsSpawned = false;

    private void Awake()
    {
        _monsterPrefabs = Resources.Load<GameObject>("Enemies/BlueSlime");
        _enemiesParent = GameObject.Find("Stage01/EnemiesPrefabs");
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

    private void SpawnMonster()
    {
        if (IsSpawned == true)
        {
            Debug.Log("monster already spawned");
            DestroyMonster();
        }
        else
        {
            Debug.Log("spawn monster");


            _spawnedMonsters = Instantiate(_monsterPrefabs, _enemiesParent.transform);
            _spawnedMonsters.transform.localPosition = new Vector3(30, 4, 0);
        }
    }

    private void DestroyMonster()
    {
        if (_spawnedMonsters != null)
        {
            Destroy(_spawnedMonsters);
        }
    }

    
}
