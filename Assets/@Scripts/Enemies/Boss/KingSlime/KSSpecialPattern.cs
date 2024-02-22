using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSSpecialPattern : MonoBehaviour
{
    private GameObject _enemy;
    private Transform[] _summonLocations;
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
            Instantiate(_enemy, location.position, Quaternion.identity);
        }
    }
}
