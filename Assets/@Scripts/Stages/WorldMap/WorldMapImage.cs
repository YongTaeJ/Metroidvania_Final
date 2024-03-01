using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapImage : MonoBehaviour
{
    [SerializeField]
    private RawImage[] _mapImageData;
    private RawImage _curActiveImage;

    [HideInInspector]
    public int _currentMapData;

    private void Awake()
    {
        _currentMapData = ItemManager.Instance.GetItemStock(ItemType.Map, 0);
        SetActiveMapImage(_currentMapData);
    }
    public void UpdateMapData()
    {
        int newMapData = ItemManager.Instance.GetItemStock(ItemType.Map, 0);

        if (_currentMapData != newMapData)
        {
            SetActiveMapImage(newMapData);
        }
    }

    private void SetActiveMapImage(int mapDataIndex)
    {
        if (_curActiveImage != null)
        {
            _curActiveImage.gameObject.SetActive(false);
        }

        if (mapDataIndex >= _mapImageData.Length)
        {
            mapDataIndex = _mapImageData.Length - 1;
        }

        _curActiveImage = _mapImageData[mapDataIndex];
        _curActiveImage.gameObject.SetActive(true);
        _currentMapData = mapDataIndex;
    }

}
