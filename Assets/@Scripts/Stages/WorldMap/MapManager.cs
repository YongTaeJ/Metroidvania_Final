using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private GameObject _worldMap;
    [SerializeField] private GameObject _worldMapUI;
    public GameObject _loadingImage;
    private Camera _mapCamera;

    public bool IsWorldMapOpen { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        CloseLargeMap();
        this.GetComponent<PlayerInput>().enabled = false;
        _mapCamera = GetComponentInChildren<Camera>();

        // 이거 두개는 묶어서 포탈 쪽으로 넘기기
        _worldMapUI.SetActive(false);
        _loadingImage.SetActive(false);

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
        Time.timeScale = 0;

        // 아래의 기능은 맵과 포탈 둘다 사용할 기능

        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.GetComponent<PlayerInput>().enabled = false;
            
            this.GetComponent<PlayerInput>().enabled = true;
            Vector3 _playerPosition = GameManager.Instance.player.transform.position;
            moveMapCamera(_playerPosition);

            //_worldMapUI.SetActive(true);
        }
    }

    public void CloseLargeMap()
    {
        _worldMap.SetActive(false);
        _worldMapUI.SetActive(false);
        IsWorldMapOpen = false;
        Time.timeScale = 1.0f;

        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.GetComponent<PlayerInput>().enabled = true;
            this.GetComponent<PlayerInput>().enabled = false;
        }
    }

    public void moveMapCamera(Vector3 position)
    {
        _mapCamera.transform.position = new Vector3(position.x, position.y + 8, position.z - 10);
    }

    // 이건 포탈 쪽으로 넘어갈 예정
    public void LoadingImage(bool isActive)
    {
        _loadingImage.SetActive(isActive);
    }
}
