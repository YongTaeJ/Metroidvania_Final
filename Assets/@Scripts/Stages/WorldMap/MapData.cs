using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapData : MonoBehaviour
{
    // 어떤 지도 데이터를 갖고 있는지
    // 갖고 있는 지도 데이터에 따라서 MapTile을 다른 것을 활성화(이전건 비활성화)

    // MapTiles - 0 : 기본 맵
    // MapTiles - 1 : 포탈 1번(Stair) 앞에서 얻는 맵
    // MapTiles - 2 : 포탈 2번(Shelter) 위에 있는 공간에서 얻는 맵(히든 요소)
    // MapTiles - 3 : Stage 2 마지막에서 얻는 맵(위쪽)

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
