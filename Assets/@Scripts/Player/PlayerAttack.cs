using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float spawnRadius = 0.7f;
    private CinemachineImpulseSource _impulseSource;
    private List<string> attackEffectPrefabs = new List<string>();
    public bool hasAttacked = false;

    /// <summary>
    /// 충돌한 Enemy와의 거리 정보를 저장하는 구조체
    /// </summary>
    private struct EnemyHitInfo
    {
        public GameObject enemy;
        public float distance;
    }

    private List<EnemyHitInfo> hitEnemies = new List<EnemyHitInfo>();

    private void Awake()
    {
        attackEffectPrefabs = new List<string>(Globals.AttackEffects);
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 충돌한 Enemy와의 거리 계산
            float distance = Vector2.Distance(transform.position, collision.transform.position);
            hitEnemies.Add(new EnemyHitInfo { enemy = collision.gameObject, distance = distance });
            ExecuteAttack();
        }
    }

    private void ExecuteAttack()
    {
        if (hitEnemies.Count == 0) return; // 충돌한 Enemy가 없으면 반환

        if (!hasAttacked)
        {
            // 가장 가까운 Enemy 찾기
            EnemyHitInfo closestEnemy = hitEnemies[0];
            foreach (var hitEnemy in hitEnemies)
            {
                if (hitEnemy.distance < closestEnemy.distance)
                {
                    closestEnemy = hitEnemy;
                }
            }

            // 가장 가까운 Enemy에 대한 공격 수행
            Attack(closestEnemy.enemy);
            GameManager.Instance.player.GainMana(5);
            // 공격 후 리스트 클리어
            hitEnemies.Clear();
        }
    }

    private void Attack(GameObject enemy)
    {
        enemy.GetComponent<IDamagable>().GetDamaged(GameManager.Instance.player._damage, GameManager.Instance.player.transform);
        StartCoroutine(HitPause(0.07f));
        CameraManager.Instance.CameraShake(_impulseSource, 1f);
        Vector2 attackPoint = enemy.transform.position;

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

        hasAttacked = true;
    }

    private IEnumerator HitPause(float pauseDuration)
    {
        Time.timeScale = 0f; // 게임 시간을 멈춤
        yield return new WaitForSecondsRealtime(pauseDuration); // 실제 시간 기준으로 일정 시간 동안 대기
        Time.timeScale = 1.0f; // 게임 시간을 원래대로 복구
    }
}
