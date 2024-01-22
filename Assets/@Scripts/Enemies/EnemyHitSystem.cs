using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyHitSystem : MonoBehaviour, IDamagable
{
    #region Fields
    private PlayerFinder _playerFinder;
    private EnemyStateMachine _stateMachine;
    private int _maxEndurance;
    private int _maxHP;
    private int _currentHP;
    private int _currentEndurance;
    private bool _isInvincible;
    #endregion

    public void Initialize(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        _maxEndurance = stateMachine.EnemyData.HitEndurance;
        _maxHP = stateMachine.EnemyData.HP;

        _currentEndurance = _maxEndurance;
        _currentHP = _maxHP;

        _playerFinder = stateMachine.PlayerFinder;
        _isInvincible = false;
    }

    public void GetDamaged(int damage, Transform target)
    {
        if(_isInvincible) return;
        
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

        float dist = _playerFinder.CurrentTransform.position.x - transform.position.x;
        float direction = dist > 0 ? -1 : 1;

        transform.DOMoveX(transform.position.x + direction * 0.4f , 0.3f);
    }
}
