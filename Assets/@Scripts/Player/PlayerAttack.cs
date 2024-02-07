using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackEffectPrefab;
    private void Awake()
    {
        attackEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/AttackEffect");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamagable>().GetDamaged(GameManager.Instance.player._damage, collision.transform);

            
            Vector2 attackPoint = collision.ClosestPoint(transform.position);

            GameObject attackEffect = PoolManager.Instance.Pop(attackEffectPrefab);
            attackEffect.transform.position = attackPoint;
        }
    }
}
