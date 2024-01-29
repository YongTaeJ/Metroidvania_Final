using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapControl : MonoBehaviour
{
    public Camera _mapCamera;

    private Vector2 _mapMove;
    private float _scrollSpeed = 0.5f;
    [SerializeField] private Vector2 _minCameraBound;
    [SerializeField] private Vector2 _maxCameraBound;


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

    public void MapSelect(InputAction.CallbackContext context)
    {

    }
    public void MapCancel(InputAction.CallbackContext context)
    {

    }
}
