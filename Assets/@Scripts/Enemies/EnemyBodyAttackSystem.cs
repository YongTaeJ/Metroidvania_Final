using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyAttackSystem : MonoBehaviour, IHasDamage
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
            StartCoroutine(RefreshCollider());
        }
    }

    private IEnumerator RefreshCollider()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _collider.enabled = true;
    }
}
