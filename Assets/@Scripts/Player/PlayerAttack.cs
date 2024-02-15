using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float spawnRadius = 0.7f;

    private List<GameObject> attackEffectPrefabs = new List<GameObject>();
    private GameObject hitParticlePrefab;
    private void Awake()
    {
        attackEffectPrefabs.Add(Resources.Load<GameObject>("Prefabs/Effects/AttackEffect1"));
        attackEffectPrefabs.Add(Resources.Load<GameObject>("Prefabs/Effects/AttackEffect2"));
        attackEffectPrefabs.Add(Resources.Load<GameObject>("Prefabs/Effects/AttackEffect3"));
        hitParticlePrefab = Resources.Load<GameObject>("Prefabs/Effects/HitParticle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamagable>().GetDamaged(GameManager.Instance.player._damage, collision.transform);

            
            Vector2 attackPoint = collision.transform.position;

            int randomIndex = Random.Range(0, attackEffectPrefabs.Count);
            GameObject attackEffectPrefab = attackEffectPrefabs[randomIndex];

            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = attackPoint + randomOffset;

            GameObject attackEffect = PoolManager.Instance.Pop(attackEffectPrefab);
            attackEffect.transform.position = spawnPosition;

            GameObject hitParticle = PoolManager.Instance.Pop(hitParticlePrefab);
            hitParticle.transform.position = spawnPosition;
        }
    }
}
