using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class KSJumpAttackState : BossAttackState
{
    private Rigidbody2D _rigidbody;
    private int _attackCount;
    private bool _isJumped;
    private float _speed;
    private GameObject _shockWaveArea;
    public KSJumpAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Random;
        _rigidbody = stateMachine.Rigidbody;
        _shockWaveArea = _attackPivot.Find("ShockWaveArea").gameObject;
        _speed = stateMachine.EnemyData.Speed;
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
            _rigidbody.velocity = Vector2.right * _direction * _speed * 3;
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
        _shockWaveArea.SetActive(false);
        _isAttackEnded = true;
    }

    private void MakeShockWave()
    {
        _shockWaveArea.SetActive(true);
        _rigidbody.isKinematic = false;
        // TODO => 쇼크웨이브 이펙트 생성 + collider 위치 고민 필요
    }
}
