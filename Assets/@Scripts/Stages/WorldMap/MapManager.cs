using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MapManager : Singleton<MapManager>
{
    public Canvas WorldMap { get; private set; }
    public Canvas Portal { get; private set; }
    public Canvas CheckCanvas { get; private set; }
    public Canvas LoadingImage { get; private set; }
    public GameObject MapTiles { get; private set; }
    public bool IsWorldMapOpen { get; private set; }
    public MapData MapData { get; private set; }


    private PlayerInput _mapInputActions;

    protected override void Awake()
    {
        base.Awake();
        AssignProperties();
    }
    private void AssignProperties()
    {
        Canvas worldMapPrefab = Resources.Load<Canvas>("Map/WorldMap");
        Canvas portalPrefab = Resources.Load<Canvas>("Map/Portal");
        Canvas mapCheckPrefab = Resources.Load<Canvas>("Map/MapCheck");
        Canvas loadingImagePrefab = Resources.Load<Canvas>("Map/LoadingImage");
        GameObject MapTilesPrefab = Resources.Load<GameObject>("Map/MapTiles");

        WorldMap = Instantiate(worldMapPrefab, transform);
        Portal = Instantiate(portalPrefab, transform);
        CheckCanvas = Instantiate(mapCheckPrefab, transform);
        LoadingImage = Instantiate(loadingImagePrefab, transform);
        MapTiles = Instantiate(MapTilesPrefab, transform);

        MapData = GetComponentInChildren<MapData>();
        _mapInputActions = GetComponentInChildren<PlayerInput>();

        if (_mapInputActions != null)
        {
            _mapInputActions.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!IsWorldMapOpen)
            {
                MapData.UpdateMapData();
                OpenLargeMap();
            }
            else
            {
                CloseLargeMap();
            }
        }
    }

    public void OpenLargeMap()
    {
        WorldMap.gameObject.SetActive(true);
        IsWorldMapOpen = true;
        Time.timeScale = 0;

        // 아래의 기능은 맵과 포탈 둘다 사용할 기능

        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.GetComponent<PlayerInput>().enabled = false;
            var MapControl = GetComponentInChildren<PlayerInput>();
            MapControl.enabled = true;
            Vector3 position = GameManager.Instance.player.transform.position;
            Camera camera = GetComponentInChildren<Camera>();
            camera.transform.position = new Vector3(position.x, position.y + 8, position.z - 10);
        }
    }

    public void CloseLargeMap()
    {
        if (GameManager.Instance.player != null)
        {
            var MapControl = GetComponentInChildren<PlayerInput>();
            MapControl.enabled = false;
            GameManager.Instance.player.GetComponent<PlayerInput>().enabled = true;
        }

        WorldMap.gameObject.SetActive(false);
        Portal.gameObject.SetActive(false);
        IsWorldMapOpen = false;
        Time.timeScale = 1.0f;
    }

    public void LoadImage(bool isActive)
    {
        LoadingImage.gameObject.SetActive(isActive);
    }
}
