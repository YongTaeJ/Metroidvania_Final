using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSDoubleAttackState : BossAttackState
{
    private int _bulletIndex;
    private GameObject _bulletPrefab;
    private int _damage;
    private int _breathCount;
    private int _currentBreathCount;
    private GameObject _breathArea;

    public KSDoubleAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternTypes = new List<BossPatternType>() {BossPatternType.Ranged, BossPatternType.Melee };
        _breathArea = _attackPivot.Find("BreathArea").gameObject;
        _bulletIndex = stateMachine.EnemyData.BulletIndex;
        _bulletPrefab = Resources.Load<GameObject>("Enemies/Bullets/EnemyBullet" + _bulletIndex.ToString() );
        _damage = stateMachine.EnemyData.Damage;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 4);

        _breathCount = Random.Range(3,6);
        _currentBreathCount = 0;

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
            return;
        }

        if(_currentBreathCount > 0)
        {
            GetDirection();
            _objectFlip.Flip(_direction);
        }
    }

    protected override void OnAttack()
    {
        _breathArea.SetActive(true);
    }

    protected override void OnAttackEnd()
    {
        _currentBreathCount++;

        if(_currentBreathCount < _breathCount)
        {
            SFX.Instance.PlayOneShot("KingSlimePullSound");
        }
        else if(_currentBreathCount == _breathCount)
        {
            _animator.SetTrigger(AnimatorHash.Attack);
            _breathArea.SetActive(false);
        }
        else
        {
            _isAttackEnded = true;
        }
    }

    private void FireBullet()
    {
        SFX.Instance.PlayOneShot("KingSlimeRangedAttackSound");
        Vector3 myPos = _attackPivot.position;

        EnemyBullet bullet = 
        GameObject.Instantiate(_bulletPrefab, myPos, Quaternion.identity)
        .GetComponent<EnemyBullet>();

        Vector3 direction = (_playerTransform.position - myPos).normalized;

        bullet.Initialize(direction, _damage);
    }
}
