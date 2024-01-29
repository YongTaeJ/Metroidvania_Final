using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private GameObject _worldMap;

    public bool IsWorldMapOpen { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        CloseLargeMap();
        this.GetComponent<PlayerInput>().enabled = false;
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

    private void OpenLargeMap()
    {
        _worldMap.SetActive(true);
        IsWorldMapOpen = true;
        StopTime();

        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.GetComponent<PlayerInput>().enabled = false;
            this.GetComponent<PlayerInput>().enabled = true;
            Debug.Log("Map move on");
        }
    }

    private void CloseLargeMap()
    {
        _worldMap.SetActive(false);
        IsWorldMapOpen = false;
        ResumeTime();

        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.GetComponent<PlayerInput>().enabled = true;
            this.GetComponent<PlayerInput>().enabled = false;
            Debug.Log("Map move off");
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
}
