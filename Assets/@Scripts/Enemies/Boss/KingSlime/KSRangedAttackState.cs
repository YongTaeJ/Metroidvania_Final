using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KSRangedAttackState : BossAttackState
{
    private int _bulletIndex;
    private GameObject _bulletPrefab;
    private int _damage;

    public KSRangedAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Ranged;   
        _bulletIndex = stateMachine.EnemyData.BulletIndex;
        Debug.Log(_bulletIndex);
        _bulletPrefab = Resources.Load<GameObject>("Enemies/Bullets/EnemyBullet" + _bulletIndex.ToString() );
        _damage = stateMachine.EnemyData.Damage;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 1);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
        if(_isAttackEnded)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }

    protected override void OnAttack()
    {
        FireBullet();
    }

    protected override void OnAttackEnd()
    {
        _isAttackEnded = true;
    }

    private void FireBullet()
    {
        Vector3 myPos = _attackPivot.position;

        EnemyBullet bullet = 
        GameObject.Instantiate(_bulletPrefab, myPos, quaternion.identity)
        .GetComponent<EnemyBullet>();

        Vector3 direction = (_playerTransform.position - myPos).normalized;

        bullet.Initialize(direction, _damage);
    }
}
