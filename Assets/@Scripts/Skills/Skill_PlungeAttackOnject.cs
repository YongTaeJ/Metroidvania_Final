using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PlungeAttackOnject : MonoBehaviour
{
    private int _damage = 50;
    private GameObject plungeAttackEffectPreFab;

    private void Awake()
    {
        plungeAttackEffectPreFab = Resources.Load<GameObject>("Prefabs/Effects/PlungeAttackEffect");
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

            Vector2 offset = new Vector2(1.5f, 0f);
            if (direction.x < 0)
            {
                offset.x *= -1;
            }

            Vector2 point = attackPoint + offset;

            GameObject plungeAttackEffect = PoolManager.Instance.Pop(plungeAttackEffectPreFab);
            plungeAttackEffect.transform.position = point;

            plungeAttackEffect.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }
    }
}
