using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SwordAurorObject : MonoBehaviour
{
    private int _damage = 5;
    private float spawnRadius = 0.7f;
    private List<string> attackEffectPrefabs = new List<string>();
    private string _attackEffectPrefab;

    private void Awake()
    {
        attackEffectPrefabs = new List<string>(Globals.AttackEffects);
        int randomIndex = Random.Range(0, attackEffectPrefabs.Count);
        _attackEffectPrefab = attackEffectPrefabs[randomIndex];
    }
    private void Start()
    {
        StartCoroutine(DestroySwordAurorObject(0.7f));
    }

    private IEnumerator DestroySwordAurorObject(float time)
    {
        yield return new WaitForSeconds(time);
        ResourceManager.Instance.Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StartCoroutine(DestroySwordAurorObject(0.1f));
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamagable>().GetDamaged(_damage, collision.transform);

            Vector2 attackPoint = collision.transform.position;

            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = attackPoint + randomOffset;

            GameObject attackEffect = ResourceManager.Instance.InstantiatePrefab(_attackEffectPrefab, pooling: true);
            attackEffect.transform.position = spawnPosition;

            GameObject hitParticle = ResourceManager.Instance.InstantiatePrefab("HitParticle", pooling: true);
            hitParticle.transform.position = spawnPosition;
        }
    }
}
