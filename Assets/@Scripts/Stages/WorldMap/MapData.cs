using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapData : MonoBehaviour
{
    // 어떤 지도 데이터를 갖고 있는지
    // 갖고 있는 지도 데이터에 따라서 MapTile을 다른 것을 활성화(이전건 비활성화)

    private Tilemap _curActiveTilemap;
    [SerializeField] public int _curMapData;

    [SerializeField] private Tilemap[] _mapTilesData;

    private void Awake()
    {
        // 아래의 itemtype을 map으로 변경해야됨. 지금은 버그로 초기화 안되서 gold 사용중
        _curMapData = ItemManager.Instance.GetItemStock(ItemType.Gold, 0);
        _curActiveTilemap = _mapTilesData[_curMapData];
        _curActiveTilemap.gameObject.SetActive(true);
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.M))
        {
            if (_curMapData >= _mapTilesData.Length)
            {
                _curMapData = _mapTilesData.Length - 1;
            }
            UpdateMapData(_curMapData);
        }
    }

    public void AcquireMap()
    {
        // 지도를 얻었을 때, 지금 갖고 있는 지도보다 mapNumber가 높으면
        // 해당 지도로 맵을 업데이트

        int acqMapData = 1; // 얻은 지도의 번호 가져오기
        if (acqMapData > _curMapData && _mapTilesData.Length > acqMapData)
        {
            _curMapData = acqMapData;
            UpdateMapData(_curMapData);
        }

        // 이 함수를 아이템을 얻는 경우에 작동하게
        
    }

    public void UpdateMapData(int mapNumber)
    {
        if (_curActiveTilemap != null)
        {
            _curActiveTilemap.gameObject.SetActive(false);
        }

        _curActiveTilemap = _mapTilesData[mapNumber];
        _curActiveTilemap.gameObject.SetActive(true);
    }
}
