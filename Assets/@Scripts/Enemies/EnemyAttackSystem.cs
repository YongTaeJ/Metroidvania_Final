using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour, IHasDamage
{
    private int _damage;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void Initialize(int damage)
    {
        _damage = damage;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.TryGetComponent<IDamagable>(out var component))
        {
            component.GetDamaged(_damage, this.transform);
        }
    }
}
