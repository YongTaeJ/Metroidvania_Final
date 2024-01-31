using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapControl : MonoBehaviour
{
    [SerializeField] private Vector2 _minCameraBound;
    [SerializeField] private Vector2 _maxCameraBound;
    [SerializeField] private Camera _mapCamera;

    private Vector2 _mapMove;
    private float _scrollSpeed = 0.1f;
    private bool IsMapCooldown = false;


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
        if (!IsMapCooldown && _mapCamera.orthographicSize <= 35)
        {
            _mapCamera.orthographicSize += 5;
            StartCoroutine(CoMapCooldown());
        }
    }
    public void MapReduce(InputAction.CallbackContext context)
    {
        if (!IsMapCooldown && _mapCamera.orthographicSize >= 15)
        {
            _mapCamera.orthographicSize -= 5;
            StartCoroutine(CoMapCooldown());
        }
    }
    
    private IEnumerator CoMapCooldown()
    {
        IsMapCooldown = true;
        yield return new WaitForSecondsRealtime(0.2f);
        IsMapCooldown = false;
    }
}
