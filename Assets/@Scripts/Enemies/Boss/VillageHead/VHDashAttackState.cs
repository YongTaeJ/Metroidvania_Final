using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VHDashAttackState : BossAttackState
{
    private Transform _transform;
    private Transform _playerTransform;
    private ObjectFlip _objectFlip;
    private float _distanceToPlayer;
    private int _startCount;
    public VHDashAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Ranged;
        _transform = stateMachine.transform;
        _playerTransform = stateMachine.PlayerFinder.CurrentTransform;
        _objectFlip = stateMachine.ObjectFlip;
        _distanceToPlayer = 1.5f;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _startCount = 0;
        _isAttackEnded = false;
        _animator.SetTrigger(AnimatorHash.Attack);
        _animator.SetInteger(AnimatorHash.PatternNumber, 2);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _animator.SetInteger(AnimatorHash.PatternNumber, -1);
    }

    public override void OnStateStay()
    {
        if(_isAttackEnded == true)
        {
            (_stateMachine as BossStateMachine).PatternTransition();
        }
    }

    protected override void OnAttack()
    {
        _startCount++;

        if(_startCount == 1)
        {
            DashToPlayer();
        }
        else if(_startCount == 2)
        {
            _attackCollider.enabled = true;
        }
    }

    protected override void OnAttackEnd()
    {
        _attackCollider.enabled = false;
        _isAttackEnded = true;
    }

    private void DashToPlayer()
    {
        float distance = _distanceToPlayer;
        float directionX = _playerTransform.position.x - _transform.position.x;

        // 플레이어보다 왼쪽에 있는 경우
        if(directionX > 0)
        {
            distance *= -1;
        }

        _objectFlip.Flip(directionX);
        _transform.DOMoveX(_playerTransform.position.x + distance, 0.7f);
    }

}
