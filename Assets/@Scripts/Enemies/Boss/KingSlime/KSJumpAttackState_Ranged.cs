using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class KSJumpAttackState_Ranged : BossAttackState
{
    private Rigidbody2D _rigidbody;
    private int _attackCount;
    private bool _isJumped;
    private float _speed;
    private int _damage;
    private GameObject _shockwave;
    public KSJumpAttackState_Ranged(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Ranged;
        _rigidbody = stateMachine.Rigidbody;
        _speed = stateMachine.EnemyData.Speed;
        _damage = stateMachine.EnemyData.Damage;
        _shockwave = Resources.Load<GameObject>("Enemies/Effects/EnemyShockwave");
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 3);
        _attackCount = 0;
        _isJumped = false;
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
            return;
        }

        if(_isJumped)
        {
            GetDirection();
            _objectFlip.Flip(_direction);
            _rigidbody.velocity = Vector2.right * _direction * _speed * 12f;
        }
    }

    protected override void OnAttack()
    {
        _attackCount++;

        if(_attackCount == 1)
        {
            _isJumped = true;
            _rigidbody.isKinematic = true;
            _rigidbody.DOMoveY(_transform.position.y + 8, 0.3f);
        }
        else if(_attackCount == 2)
        {
            _isJumped = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.DOMoveY(_transform.position.y - 8, 0.3f);
            TimerManager.Instance.StartTimer(0.3f, MakeShockWave);
        }
    }

    protected override void OnAttackEnd()
    {
        _isAttackEnded = true;
    }

    private void MakeShockWave()
    {
        _rigidbody.isKinematic = false;
        SFX.Instance.PlayOneShot("KingSlimeJumpAttackSound", 0.3f);
        GameObject.Instantiate(_shockwave, _transform.position, quaternion.identity)
        .GetComponent<EnemyAttackSystem>().Initialize(_damage);
    }
}
