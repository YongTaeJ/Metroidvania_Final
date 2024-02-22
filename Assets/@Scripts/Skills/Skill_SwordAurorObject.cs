using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SwordAurorObject : MonoBehaviour
{
    private int _damage = 5;
    private float spawnRadius = 0.7f;
    private List<string> attackEffectPrefabs = new List<string>();
    private string _attackEffectPrefab;
    private CinemachineImpulseSource _impulseSource;
    private int enemyLayer;


    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        attackEffectPrefabs = new List<string>(Globals.AttackEffects);
        int randomIndex = Random.Range(0, attackEffectPrefabs.Count);
        _attackEffectPrefab = attackEffectPrefabs[randomIndex];
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            collision.GetComponent<IDamagable>().GetDamaged(_damage, collision.transform);

            CameraManager.Instance.CameraShake(_impulseSource, 1f);
            Vector2 attackPoint = collision.transform.position;

            int randomIndex = Random.Range(0, attackEffectPrefabs.Count);
            string attackEffectPrefab = attackEffectPrefabs[randomIndex];

            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = attackPoint + randomOffset;

            GameObject attackEffect = ResourceManager.Instance.InstantiatePrefab(attackEffectPrefab, pooling: true);
            attackEffect.transform.position = spawnPosition;

            float hitParticleScale = GameManager.Instance.player._controller.IsFacingRight ? 1.0f : -1.0f;

            GameObject hitParticle = ResourceManager.Instance.InstantiatePrefab("HitParticle", pooling: true);
            hitParticle.GetComponent<ParticleMaterialChanger>().ChangeMaterial(collision.tag);
            hitParticle.transform.position = spawnPosition;
            hitParticle.transform.localScale = new Vector3(hitParticleScale, 1, 1);
        }
    }
}
