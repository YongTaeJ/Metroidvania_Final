using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlip
{
    private Transform _transform;
    private Vector3 _originScale;
    private Vector3 _flipScale;
    private bool _isFliped;

    public ObjectFlip(Transform transform)
    {
        _transform = transform;
        _originScale = transform.localScale;
        _flipScale = new Vector3 (_originScale.x * -1, _originScale.y, _originScale.z);
        _isFliped = false;
    }

    public void Flip(float direction)
    {
        if(direction > 0)
        {
            _transform.localScale = _originScale;
        }
        else if(direction < 0)
        {
            _transform.localScale = _flipScale;
        }
    }
}
