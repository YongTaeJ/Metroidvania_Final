using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Transform _attackPivot;
    private Vector2 _pivotRightPosition;
    private Vector2 _pivotLeftPosition;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        _attackPivot = transform.Find("AttackPivot");
        _pivotRightPosition = _attackPivot.localPosition;
        _pivotLeftPosition = _attackPivot.localPosition * -1;
    }

    private void FixedUpdate()
    {
        if(_rigidbody.velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
            _attackPivot.localPosition = _pivotRightPosition;
        }
        else if(_rigidbody.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
            _attackPivot.localPosition = _pivotLeftPosition;
        }
    }
}
