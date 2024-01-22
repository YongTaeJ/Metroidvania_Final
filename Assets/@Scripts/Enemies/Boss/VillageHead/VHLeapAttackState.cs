using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHLeapAttackState : BossAttackState
{
    private int _startCount;
    private int _endCount; 
    private Transform _playerTransform;
    private Transform _transform;
    private Collider2D _throwCollider;
    private ObjectFlip _objectFlip;
    private float _throwDistance;
    private float _jumpTime;
    private float _direction;
    public VHLeapAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Random;

        _throwCollider = stateMachine.transform.Find("Sprite/AttackPivot/ThrowAttack").GetComponent<Collider2D>();
        _playerTransform = stateMachine.PlayerFinder.transform;
        _transform = stateMachine.transform;
        _objectFlip = stateMachine.ObjectFlip;
        
        _jumpTime = 2.5f;
        _throwDistance = 4.5f;
    }

    public override void OnStateEnter()
    {
        _animator.SetInteger(AnimatorHash.PatternNumber, 3);
        _startCount = 0;
        _endCount = 0;
    }

    public override void OnStateExit()
    {
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
    }

    protected override void OnAttack()
    {
        _startCount++;

        if(_startCount == 1)
        {
            _direction = _playerTransform.position.x - _transform.position.x > 0 ? -1 : 1 ;
            _objectFlip.Flip(_direction);
            _transform.DOMoveX(_playerTransform.position.x + _throwDistance * _direction, _jumpTime);
        }
        else if(_startCount == 2)
        {
            _throwCollider.enabled = true;
        }
        else if(_startCount == 3)
        {
            // TODO => 방향 검사 필요
            _transform.position += Vector3.right * _direction * _throwDistance;
        }
    }

    protected override void OnAttackEnd()
    {
        _endCount++;

        if(_endCount == 1)
        {
            _throwCollider.enabled = false;
        }
        else if(_endCount == 2)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }
}
