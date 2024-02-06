using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SwordAurorObject : MonoBehaviour
{
    private int _damage = 50;

    void Start()
    {
        StartCoroutine(DestroySwordAurorObject());
    }

    private IEnumerator DestroySwordAurorObject()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamagable>().GetDamaged(_damage, collision.transform);

            Vector2 attackPoint = collision.ClosestPoint(transform.position);

            //너무 긴데 이걸 매니져로 만들어서 여러개 관리하는게 효율적인가?
            GameObject attackEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/AttackEffect");
            GameObject attackEffect = PoolManager.Instance.Pop(attackEffectPrefab);
            attackEffect.transform.position = attackPoint;
        }
    }
}
