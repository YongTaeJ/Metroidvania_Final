using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MapManager : Singleton<MapManager>
{
    public Canvas WorldMap { get; private set; }
    public Canvas PortalMap { get; private set; }
    public Canvas BuyPortal { get; private set; }
    public Canvas LoadingImage { get; private set; }
    //public GameObject MapImages { get; private set; }
    public bool IsWorldMapOpen { get; private set; }

    private WorldMapImage _worldMapImage;
    private PortalMapImage _portalMapImage;
    private PlayerInput _mapInputActions;

    protected override void Awake()
    {
        base.Awake();
        AssignProperties();
    }
    private void AssignProperties()
    {
        Canvas worldMapPrefab = Resources.Load<Canvas>("Map/WorldMap");
        Canvas portalMapPrefab = Resources.Load<Canvas>("Map/PortalMap");
        Canvas buyPortalPrefab = Resources.Load<Canvas>("Map/BuyPortal");
        Canvas loadingImagePrefab = Resources.Load<Canvas>("Map/LoadingImage");

        WorldMap = Instantiate(worldMapPrefab, transform);
        PortalMap = Instantiate(portalMapPrefab, transform);
        BuyPortal = Instantiate(buyPortalPrefab, transform);
        LoadingImage = Instantiate(loadingImagePrefab, transform);

        _worldMapImage = WorldMap.GetComponent<WorldMapImage>();
        _portalMapImage = WorldMap.GetComponent<PortalMapImage>();
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
                _worldMapImage.UpdateMapData();
                OpenLargeMap();
            }
            else
            {
                CloseLargeMap();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsWorldMapOpen)
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

    public void OpenPortalMap()
    {
        PortalMap.gameObject.SetActive(true);
        IsWorldMapOpen = true;
        Time.timeScale = 0;

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
        PortalMap.gameObject.SetActive(false);
        IsWorldMapOpen = false;
        Time.timeScale = 1.0f;

        if (LoadingImage.gameObject.activeSelf)
        {
            Invoke("LoadImageClose", 1.5f);
        }
    }

    public void LoadImage(bool isActive)
    {
        LoadingImage.gameObject.SetActive(isActive);
    }
    public void LoadImageClose()
    {
        LoadingImage.gameObject.SetActive(false);
    }

    public void ActivateBuyPortalUI(int portalIndex, int portalPrice)
    {
        BuyPortal buyPortalScript = BuyPortal.GetComponentInChildren<BuyPortal>();
        if (buyPortalScript != null)
        {
            buyPortalScript.ActivateUI(portalIndex, portalPrice);
        }
    }
}
