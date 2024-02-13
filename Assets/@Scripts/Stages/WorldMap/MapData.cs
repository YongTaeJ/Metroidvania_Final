using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapData : MonoBehaviour
{
    // 어떤 지도 데이터를 갖고 있는지
    // 갖고 있는 지도 데이터에 따라서 MapTile을 다른 것을 활성화(이전건 비활성화)

    private Tilemap[] _mapTilesData;
    private Tilemap _curActiveTilemap;

    public GameObject mapTilesContainer;

    [HideInInspector]
    public int _curMapData;

    private void Awake()
    {
        InitializeMapTilesData();
        _curMapData = ItemManager.Instance.GetItemStock(ItemType.Map, 0);
        _curActiveTilemap = _mapTilesData[_curMapData];
        _curActiveTilemap.gameObject.SetActive(true);
    }

    private void InitializeMapTilesData()
    {
        if (mapTilesContainer != null)
        {
            foreach (Transform mapTile in mapTilesContainer.transform)
            {
                Tilemap[] mapTiles = mapTilesContainer.GetComponentsInChildren<Tilemap>(true);
                _mapTilesData = mapTiles;
            }
        }
    }

    public void UpdateMapData()
    {
        int _newMapData = ItemManager.Instance.GetItemStock(ItemType.Map, 0);

        if (_curMapData != _newMapData)
        {
            _curMapData = _newMapData;
        }

        if (_curActiveTilemap != null)
        {
            _curActiveTilemap.gameObject.SetActive(false);
        }

        if (_curMapData >= _mapTilesData.Length)
        {
            _curMapData = _mapTilesData.Length - 1;
        }

        _curActiveTilemap = _mapTilesData[_curMapData];
        _curActiveTilemap.gameObject.SetActive(true);
    }
}
