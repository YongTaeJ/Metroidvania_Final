using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSpawn : MonoBehaviour
{
    private MonsterSpawner[] _monsterSpawners;

    private void Awake()
    {
        _monsterSpawners = GetComponentsInChildren<MonsterSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var monsterSpawner in _monsterSpawners)
            {
                monsterSpawner.IsClear = false;
            }
        }
    }
}
