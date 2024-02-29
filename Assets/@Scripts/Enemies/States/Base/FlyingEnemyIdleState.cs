using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingEnemyIdleState : EnemyBaseState
{
    private PlayerFinder _playerFinder;
    private Transform _transform;
    private Vector2 _startPosition;
    private float _wanderTime;
    private float _currentTime;
    public FlyingEnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _playerFinder = stateMachine.PlayerFinder;
        _startPosition = stateMachine.transform.position;
        _transform = stateMachine.transform;
        _wanderTime = 5f;
    }

    public override void OnStateEnter()
    {
        _currentTime = Random.Range(0f, 2f);
        _animator.SetBool(AnimatorHash.Idle, true);
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Idle, false);
        _transform.DOKill();
    }

    public override void OnStateStay()
    {
        if(_playerFinder.IsPlayerEnter)
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Chase]);
        }

        if (_playerFinder.CurrentTransform)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _wanderTime)
            {
                _currentTime = 0f;
                RandomPatrol();
            }
        }
    }

    private void RandomPatrol()
    {
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-1.5f, 1.5f);

        Vector2 patrolPosition = _startPosition + new Vector2(x,y);

        _transform.DOMove(patrolPosition, _wanderTime);
    }
}
