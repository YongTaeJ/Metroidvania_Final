using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderState : EnemyFindingState
{
    private Transform _myTransform;
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private float _walkTime;
    private int _layerMask;
    public EnemyWanderState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _layerMask = LayerMask.GetMask("Ground");
        _rigidbody = stateMachine.Rigidbody;
        _myTransform = stateMachine.transform;
    }

    #region IState

    public override void OnStateEnter()
    {
        _animator.SetBool(AnimatorHash.Walk, true);
        _direction = Random.Range(0, 1f) > 0.5f ? Vector2.right : Vector2.left;
        _stateTime = Random.Range(0.5f, 1.5f);
    }

    public override void OnStateExit()
    {
        _animator.SetBool(AnimatorHash.Walk, false);
    }

    public override void OnStateStay()
    {
        base.OnStateStay();

        if(IsTimeOut()) return;

        FallCheck();
        _rigidbody.velocity = GetVelocity();
    }

    #endregion

    private bool IsTimeOut()
    {
        _stateTime -= Time.deltaTime;
        if(_stateTime <= 0)
        {
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Idle]);
            return true;
        }
        else
            return false;
    }

    private void FallCheck()
    {
        Vector2 currentPosition = _myTransform.position;

        bool isLand = Physics2D.Raycast(currentPosition + _direction, Vector2.down, 1f, _layerMask);

        if(!isLand) _direction *= -1;
    }

    private Vector2 GetVelocity()
    {
        return new Vector2 (_direction.x * _stateMachine.EnemyData.Speed, _rigidbody.velocity.y); 
    }
}
