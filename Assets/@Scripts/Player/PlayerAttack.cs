using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player _player;
    private EnemyHitSystem _enemyHit;
    private PolygonCollider2D _polygonCollider;

    private void Awake()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _enemyHit.GetDamaged(_player._damage);
        }
    }
}
