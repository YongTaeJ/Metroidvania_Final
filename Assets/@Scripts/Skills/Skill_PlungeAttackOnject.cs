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

    private void Start()
    {
        StartCoroutine(DestroySwordAurorObject(0.3f));
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
            StartCoroutine(HitPause(0.07f));

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
            hitParticle.transform.position = spawnPosition;
            hitParticle.transform.localScale = new Vector3(hitParticleScale, 1, 1);
        }
    }

    private IEnumerator HitPause(float pauseDuration)
    {
        Time.timeScale = 0f; // 게임 시간을 멈춤
        yield return new WaitForSecondsRealtime(pauseDuration); // 실제 시간 기준으로 일정 시간 동안 대기
        Time.timeScale = 1.0f; // 게임 시간을 원래대로 복구
    }
}
