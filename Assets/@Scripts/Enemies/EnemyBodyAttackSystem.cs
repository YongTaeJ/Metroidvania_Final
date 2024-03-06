using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyAttackSystem : MonoBehaviour, IHasDamage
{
    private int _damage;
    public Collider2D Collider {get; private set;}

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
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
        Collider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Collider.enabled = true;
    }
}
