using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PlungeAttackOnject : MonoBehaviour
{
    private int _damage = 5;
    private float spawnRadius = 0.7f;
    private List<string> attackEffectPrefabs = new List<string>();
    private string _attackEffectPrefab;
    private CinemachineImpulseSource _impulseSource;


    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        attackEffectPrefabs = new List<string>(Globals.AttackEffects);
        int randomIndex = Random.Range(0, attackEffectPrefabs.Count);
        _attackEffectPrefab = attackEffectPrefabs[randomIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamagable>().GetDamaged(_damage, collision.transform);

            CameraManager.Instance.CameraShake(_impulseSource, 1f);
            Vector2 attackPoint = collision.transform.position;

            int randomIndex = Random.Range(0, attackEffectPrefabs.Count);

            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = attackPoint + randomOffset;

            GameObject attackEffect = ResourceManager.Instance.InstantiatePrefab(_attackEffectPrefab, pooling: true);
            attackEffect.transform.position = spawnPosition;

            float hitParticleScale = GameManager.Instance.player._controller.IsFacingRight ? 1.0f : -1.0f;

            GameObject hitParticle = ResourceManager.Instance.InstantiatePrefab("HitParticle", pooling: true);
            hitParticle.GetComponent<ParticleMaterialChanger>().ChangeMaterial(collision.name);
            hitParticle.transform.position = spawnPosition;
            hitParticle.transform.localScale = new Vector3(hitParticleScale, 1, 1);
        }
    }
}
