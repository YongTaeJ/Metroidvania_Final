using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BackgroundStay : MonoBehaviour
{
    [SerializeField] private GameObject[] _layers;
    [SerializeField] private float _layerLength;

    private Vector3 _lastPlayerPosition;
    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = GameManager.Instance.player.transform;
        _lastPlayerPosition = _playerTransform.position;
    }

    private void Update()
    {
        Vector3 deltaMovement = _playerTransform.position - _lastPlayerPosition;

        for (int i = 0; i < _layers.Length; i++)
        {
            if (deltaMovement.x != 0)
            {
                if (deltaMovement.x > 0 && _layers[i].transform.position.x < _lastPlayerPosition.x - 1.5 * _layerLength)
                {
                    Vector3 newPos = _layers[i].transform.position;
                    newPos.x += 3 * _layerLength;
                    _layers[i].transform.position = newPos;
                }
                else if (deltaMovement.x < 0 && _layers[i].transform.position.x > _lastPlayerPosition.x + 1.5 * _layerLength)
                {
                    Vector3 newPos = _layers[i].transform.position;
                    newPos.x -= 3 * _layerLength;
                    _layers[i].transform.position = newPos;
                }
            }

            if (deltaMovement.y != 0)
            {
                _layers[i].transform.Translate(Vector3.left * Time.deltaTime);

                if (deltaMovement.y > 0 && _layers[i].transform.position.y < _lastPlayerPosition.y - _layerLength)
                {
                    Vector3 newPos = _layers[i].transform.position;
                    newPos.y += 2 *_layerLength;
                    _layers[i].transform.position = newPos;
                }
                else if (deltaMovement.y < 0 && _layers[i].transform.position.y > _lastPlayerPosition.y + _layerLength)
                {
                    Vector3 newPos = _layers[i].transform.position;
                    newPos.y -= 2 *_layerLength;
                    _layers[i].transform.position = newPos;
                }
            }            
        }
        _lastPlayerPosition = _playerTransform.position;
    }
}
