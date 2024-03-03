using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHSpinAttackState : BossAttackState
{
    private int _attackCount;
    private int _endCount;
    private int _rotateCount;
    private bool _isChase;
    private float _speed;
    private Collider2D _spinCollider;
    private Transform _enemyTransform;
    private Rigidbody2D _rigidbody;
    public VHSpinAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternTypes = new List<BossPatternType>(){BossPatternType.Random};

        _spinCollider = stateMachine.transform.Find("Sprite/AttackPivot/SpinAttack").GetComponent<Collider2D>();
        _enemyTransform = stateMachine.transform;
        _rigidbody = stateMachine.Rigidbody;
        _speed = stateMachine.EnemyData.Speed * 6.5f;
        _rotateCount = 10;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 4);
        _attackCount = 0;
        _endCount = 0;
        _isChase = false;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _spinCollider.enabled = false;
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
        if(_isChase)
        {
            float direction =
            _playerTransform.position.x - _enemyTransform.position.x > 0 ?
            1f : -1f ;
            _rigidbody.velocity = new Vector2(direction * _speed , _rigidbody.velocity.y);
        }
    }

    protected override void OnAttack()
    {
        SFX.Instance.PlayOneShot("VillageHeadSpinAttackSound", 0.5f);
        _attackCount++;

        if(_attackCount == _rotateCount)
        {
            _isChase = false;
            _spinCollider.enabled = false;
            _animator.SetInteger(AnimatorHash.PatternNumber, -2);
        }
    }

    protected override void OnAttackEnd()
    {
        _endCount++;

        if(_endCount == 1)
        {
            _isChase = true;
            _spinCollider.enabled = true;
        }
        else if(_endCount == 2)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }
}
