using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private GameObject _rockCrashEffect;

    public void Initialize( Sprite sprite, float x, float y )
    {
        _rockCrashEffect = Resources.Load<GameObject>("Enemies/Effects/RockCrashEffect");

        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        _spriteRenderer.sprite = sprite;
        _collider.size = new Vector2 (x,y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player") && other.gameObject.TryGetComponent<IDamagable>(out var component))
        {
            component.GetDamaged(1, transform);
            Instantiate(_rockCrashEffect, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
        else if(other.collider.CompareTag("Ground"))
        {
            Instantiate(_rockCrashEffect, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
}

