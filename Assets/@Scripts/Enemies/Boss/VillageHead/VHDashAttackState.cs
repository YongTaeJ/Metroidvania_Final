using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VHDashAttackState : BossAttackState
{
    private Transform _transform;
    private Transform _playerTransform;
    private float _distanceToPlayer;
    public VHDashAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        BossPatternType = BossPatternType.Ranged;
        _transform = stateMachine.transform;
        _playerTransform = stateMachine.PlayerFinder.transform;
        _distanceToPlayer = 1.5f;
    }

    public override void OnStateEnter()
    {
        _isAttackEnded = false;
        _animator.SetInteger(AnimatorHash.PatternNumber, 2);
        DashToPlayer();
    }

    public override void OnStateExit()
    {
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
        _attackCollider.enabled = true;
    }

    protected override void OnAttackEnd()
    {
        _isAttackEnded = true;
        _attackCollider.enabled = false;
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

        Vector3 dist = Vector2.right * distance;

        _transform.DOMove(_playerTransform.position + dist, 0.4f);
    }

}
