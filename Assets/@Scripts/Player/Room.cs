using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject virtualcam;
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private void Awake()
    {
        _virtualCamera = virtualcam.GetComponent<CinemachineVirtualCamera>();
        _framingTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        if (_framingTransposer != null)
        {
            Vector3 trackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
            trackedObjectOffset.x = GameManager.Instance.player._controller.MoveInput().x;
            _framingTransposer.m_TrackedObjectOffset = trackedObjectOffset;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            CameraManager.Instance.GetCamera(_virtualCamera);
            _framingTransposer.m_XDamping = 1f;
            virtualcam.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualcam.SetActive(false);
        }
    }
}
