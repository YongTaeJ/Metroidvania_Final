using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSSpecialPattern : MonoBehaviour
{
    private GameObject _enemy;
    private Transform[] _summonLocations;
    private List<GameObject> _spawnedEnemies = new List<GameObject>();
    private void Awake()
    {
        _enemy = Resources.Load<GameObject>("Enemies/Monsters/BlueSlime");
        _summonLocations = new Transform[2];
        _summonLocations[0] = transform.Find("SummonLocation1");
        _summonLocations[1] = transform.Find("SummonLocation2");
    }

    public void SummonEnemies()
    {
        foreach(var location in _summonLocations)
        {
            SFX.Instance.PlayOneShot("SlimeSummonSound");
            GameObject spawnedEnemy = Instantiate(_enemy, location.position, Quaternion.identity);
            _spawnedEnemies.Add(spawnedEnemy);
        }
    }

    public void DestroyEnemies()
    {
        foreach (GameObject GO in _spawnedEnemies)
        {
            Destroy(GO);
        }
        _spawnedEnemies.Clear();
    }
}
