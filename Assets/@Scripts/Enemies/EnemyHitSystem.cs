using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitSystem : IDamagable
{
    #region Fields
    private PlayerFinder _playerFinder;
    private Rigidbody2D _rigidbody;
    private EnemyStateMachine _stateMachine;
    private int _maxEndurance;
    private int _maxHP;
    private int _currentHP;
    private int _currentEndurance;
    #endregion

    public EnemyHitSystem(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _rigidbody = stateMachine.Rigidbody;

        _maxEndurance = stateMachine.EnemyData.HitEndurance;
        _maxHP = stateMachine.EnemyData.HP;

        _currentEndurance = _maxEndurance;
        _currentHP = _maxHP;

        _playerFinder = stateMachine.PlayerFinder;
        _rigidbody = stateMachine.Rigidbody;
    }

    public void GetDamaged(int damage)
    {
        _currentEndurance--;
        _currentHP -= damage;

        if(_currentHP <= 0)
        {
            KnockBack();
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Dead]);
            return;
        }

        if(_currentEndurance <= 0)
        {
            KnockBack();
            _currentEndurance = _maxEndurance;
            _stateMachine.StateTransition(_stateMachine.StateDictionary[EnemyStateType.Hurt]);
            return;
        }
    }

    private void KnockBack()
    {
        if(_playerFinder.CurrentTransform == null) return;

        float dist = _playerFinder.CurrentTransform.position.x - _rigidbody.position.x;

        Vector2 direction = dist > 0 ? Vector2.left : Vector2.right;

        _rigidbody.AddForce(direction * 100);
    }
}
