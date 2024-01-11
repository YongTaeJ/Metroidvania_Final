using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_rigidbody.velocity.x> 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if(_rigidbody.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
