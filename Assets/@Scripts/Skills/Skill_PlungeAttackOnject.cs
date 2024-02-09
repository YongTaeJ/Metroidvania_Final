using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PlungeAttackOnject : MonoBehaviour
{
    private int _damage = 50;
    private GameObject attackEffectPreFab;

    private void Awake()
    {
        attackEffectPreFab = Resources.Load<GameObject>("Prefabs/Effects/AttackEffect");
    }

    private void Start()
    {
        StartCoroutine(DestroySwordAurorObject());
    }

    private IEnumerator DestroySwordAurorObject()
    {
        yield return new WaitForSeconds(0.3f);
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
            Vector2 direction = attackPoint - (Vector2)transform.position;
            GameObject attackEffect = PoolManager.Instance.Pop(attackEffectPreFab);
            attackEffect.transform.position = attackPoint;

            attackEffect.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }
    }
}
