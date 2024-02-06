using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamagable>().GetDamaged(GameManager.Instance.player._damage, collision.transform);

            
            Vector2 attackPoint = collision.ClosestPoint(transform.position);

            //너무 긴데 이걸 매니져로 만들어서 여러개 관리하는게 효율적인가?
            attackEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/AttackEffect");
            GameObject attackEffect = PoolManager.Instance.Pop(attackEffectPrefab);
            attackEffect.transform.position = attackPoint;
        }
    }
}
