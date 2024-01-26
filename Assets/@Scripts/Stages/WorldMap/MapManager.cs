using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private GameObject _largeMap;

    public bool IsLargeMapOpen { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        CloseLargeMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!IsLargeMapOpen)
            {
                OpenLargeMap();
            }
            else
            {
                CloseLargeMap();
            }
        }
    }

    private void OpenLargeMap()
    {
        _largeMap.SetActive(true);
        IsLargeMapOpen = true;
    }

    private void CloseLargeMap()
    {
        _largeMap.SetActive(false);
        IsLargeMapOpen = false;
    }
}
