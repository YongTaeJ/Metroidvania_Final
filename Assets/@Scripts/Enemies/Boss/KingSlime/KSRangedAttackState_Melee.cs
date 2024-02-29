using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KSRangedAttackState_Melee : BossAttackState
{
    private int _bulletIndex;
    private GameObject _bulletPrefab;
    private int _damage;

    public KSRangedAttackState_Melee(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Melee;   
        _bulletIndex = stateMachine.EnemyData.BulletIndex;
        _bulletPrefab = Resources.Load<GameObject>("Enemies/Bullets/EnemyBullet" + _bulletIndex.ToString() );
        _damage = stateMachine.EnemyData.Damage;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 1);

        _eventReceiver.OnBulletFire -= FireBullet;
        _eventReceiver.OnBulletFire += FireBullet;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);

        _eventReceiver.OnBulletFire -= FireBullet;
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
        // X
    }

    protected override void OnAttackEnd()
    {
        _isAttackEnded = true;
    }

    private void FireBullet()
    {
        SFX.Instance.PlayOneShot("KingSlimeRangedAttackSound");
        Vector3 myPos = _attackPivot.position;

        EnemyBullet bullet = 
        GameObject.Instantiate(_bulletPrefab, myPos, quaternion.identity)
        .GetComponent<EnemyBullet>();

        Vector3 direction = (_playerTransform.position - myPos).normalized;

        bullet.Initialize(direction, _damage);
    }
}
