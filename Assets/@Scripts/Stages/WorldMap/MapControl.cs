using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class MapControl : MonoBehaviour
{
    private Vector2 _minCameraBound;
    private Vector2 _maxCameraBound;
    public Camera _mapCamera;

    private Vector2 _mapMove;
    private float _scrollSpeed = 0.5f;

    private void Awake()
    {
        _minCameraBound = new Vector2(-25, -200);
        _maxCameraBound = new Vector2(490, 50);
        _mapCamera = GetComponentInChildren<Camera>();
    }


    private void Update()
    {
        Vector3 cameraMovement = new Vector3(_mapMove.x, _mapMove.y, 0) * _scrollSpeed;
        Vector3 newCameraPosition = _mapCamera.transform.position + cameraMovement;
        newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, _minCameraBound.x, _maxCameraBound.x);
        newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, _minCameraBound.y, _maxCameraBound.y);

        _mapCamera.transform.position = newCameraPosition;
    }


    public void MapMove(InputAction.CallbackContext context)
    {
        _mapMove = context.ReadValue<Vector2>();
    }

    public void MapEnlarge(InputAction.CallbackContext context)
    {
        if (enabled && context.performed && _mapCamera.orthographicSize >= 15)
        {
            _mapCamera.orthographicSize -= 4;
        }
    }
    public void MapReduce(InputAction.CallbackContext context)
    {
        if (enabled && context.performed && _mapCamera.orthographicSize <= 35)
        {
            _mapCamera.orthographicSize += 4;
        }
    }
}
