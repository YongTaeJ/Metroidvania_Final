using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private GameObject _worldMap;
    [SerializeField] private GameObject _worldMapUI;
    public PortalTrigger _portalTrigger;
    public GameObject _loadingImage;
    private Camera _mapCamera;

    public bool IsWorldMapOpen { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        CloseLargeMap();
        this.GetComponent<PlayerInput>().enabled = false;
        _worldMapUI.SetActive(false);
        _loadingImage.SetActive(false);
        _mapCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!IsWorldMapOpen)
            {
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
        _worldMap.SetActive(true);
        IsWorldMapOpen = true;
        StopTime();

        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.GetComponent<PlayerInput>().enabled = false;
            
            this.GetComponent<PlayerInput>().enabled = true;
            Vector3 _playerPosition = GameManager.Instance.player.transform.position;
            moveMapCamera(_playerPosition);
            _worldMapUI.SetActive(true);
        }

        //if (_portalTrigger != null && _portalTrigger.CanUsePortal)
        //{
        //    _worldMapUI.SetActive(true);
        //}
    }

    public void CloseLargeMap()
    {
        _worldMap.SetActive(false);
        _worldMapUI.SetActive(false);
        IsWorldMapOpen = false;
        ResumeTime();

        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.GetComponent<PlayerInput>().enabled = true;
            this.GetComponent<PlayerInput>().enabled = false;
        }
    }

    /// <summary>
    /// 시간을 정지하는 기능 - 일시정지 기능을 만들면 그 것을 사용하고
    /// 이 기능은 그것을 상속받아서 사용하거나 같으면 참조해서 사용하게
    /// </summary>
    private void StopTime()
    {
        Time.timeScale = 0;
        //AudioListener.pause = true;  // 사운드효과 사용시
    }

    private void ResumeTime()
    {
        Time.timeScale = 1.0f;
        //AudioListener.pause = false; // 사운드효과 사용시
    }

    public void moveMapCamera(Vector3 position)
    {
        _mapCamera.transform.position = new Vector3(position.x, position.y + 8, position.z - 10);
    }

    public void LoadingImage(bool isActive)
    {
        _loadingImage.SetActive(isActive);
    }
}
