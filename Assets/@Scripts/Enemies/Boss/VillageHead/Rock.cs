using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    public void Initialize( Sprite sprite, float x, float y )
    {
        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        _spriteRenderer.sprite = sprite;
        _collider.size = new Vector2 (x,y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<IDamagable>().GetDamaged(1, transform);
            Destroy(gameObject);
        }
        else if(other.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}

